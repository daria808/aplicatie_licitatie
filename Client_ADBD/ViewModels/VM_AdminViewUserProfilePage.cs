using Client_ADBD.Models;
using Client_ADBD.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client_ADBD.ViewModels
{
    internal class VM_AdminViewUserProfilePage : VM_Base
    {

        public ICommand BackCommand { get; set; }

        public string _sortFilter;
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommad { get; }
        public ICommand AddAuctionCommand { get; }

        public string SelectedSortOption
        {
            get => _sortFilter;
            set
            {
                _sortFilter = value;
                if (string.IsNullOrEmpty(Status))
                {
                    ReloadAuctions(SelectedSortOption);
                }
                else
                {
                    ReloadAuctions(SelectedSortOption, Status);
                }
                OnPropertyChange(nameof(SelectedSortOption));
            }
        }

        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                Helpers.Utilities.Status = value;

                if (string.IsNullOrEmpty(SelectedSortOption))
                {
                    ReloadAuctions(Status);
                }
                else
                {
                    ReloadAuctions(SelectedSortOption, Status);
                }

                OnPropertyChange(nameof(Status));
            }
        }

        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public User_ CurrentUser;
        public VM_AdminViewUserProfilePage(User_ user, string sortFilter = "default", string statusFilter = "default")
        {
            CurrentUser = user;
            Nume = user._lastName;
            Prenume = user._firstName;
            Username = user._username;
            Email = user._email;
            BackCommand = new RelayCommand(OnBack);
            SelectedSortOption = sortFilter;
            Status = statusFilter;
            NextPageCommand = new RelayCommand(NextPage);
            PreviousPageCommad = new RelayCommand(PreviousPage);
            AddAuctionCommand = new RelayCommand(ShowAddAuction);
            Helpers.Utilities.OnStatusChanged += (sender, e) =>
            {
                Status = Helpers.Utilities.Status;
            };

            Helpers.Timer.AddEventToTimer(UpdatePostStatus, -1, 5);
            Helpers.Timer.AddEventToTimer(UpdateTimeLeftForDisplayedAuctions, 1, -1);
        }

        private void UpdatePostStatus()
        {
            DatabaseManager.UpdatePostStatus();
        }

        private int _currentPage = 1;
        private readonly int _itemsPerPage = 5;

        private ObservableCollection<Auction_> _auctions;
        public ObservableCollection<Auction_> Auctions
        {
            get { return _auctions; }
            set
            {
                if (_auctions != value)
                {
                    _auctions = value;
                    OnPropertyChange(nameof(Auctions));
                }
            }
        }

        private ObservableCollection<VM_AuctionControler> _vmAuctions;
        public ObservableCollection<VM_AuctionControler> VM_Auctions
        {
            get { return _vmAuctions; }
            set
            {
                if (_vmAuctions != value)
                {
                    _vmAuctions = value;
                    OnPropertyChange(nameof(VM_Auctions));
                }
            }
        }

        private ObservableCollection<VM_AuctionControler> _displayedAuctions;
        public ObservableCollection<VM_AuctionControler> DisplayedAuctions
        {
            get { return _displayedAuctions; }
            set
            {
                if (_displayedAuctions != value)
                {
                    _displayedAuctions = value;
                    OnPropertyChange(nameof(DisplayedAuctions));
                }
            }
        }

        public void SetAuctions(int userId, string statusFilter = "default", string sortFilter = "default")
        {
            // Obține licitațiile utilizatorului specificat
            List<Auction_> auctions = DatabaseManager.GetAuctionsByUserId(userId, statusFilter, sortFilter);

            if (_vmAuctions == null || DisplayedAuctions.Count() == 0)
            {
                // Mapează licitațiile într-o listă de obiecte VM_AuctionControler
                _vmAuctions = new ObservableCollection<VM_AuctionControler>(
                    auctions.Select(a => new VM_AuctionControler
                    {
                        Id = a.id,
                        Name = a.name,
                        StartTime = a.startTime,
                        EndTime = a.endTime,
                        Location = a.location,
                        Status = a.statusStr,
                        ImagePath = a.imagePath,
                        Number = a.auctionNumber,
                    })
                );

                // Actualizează timpul rămas pentru fiecare licitație
                foreach (var auctionViewModel in _vmAuctions)
                {
                    auctionViewModel.UpdateTimeLeft();
                }
            }

            Auctions = new ObservableCollection<Auction_>(auctions);
            _currentPage = 1;
            UpdateDisplayedAuctions();
        }


        private void UpdateDisplayedAuctions()
        {
            if (Auctions == null || VM_Auctions == null) return;

            var pageAuctions = Auctions.Skip((_currentPage - 1) * _itemsPerPage).Take(_itemsPerPage);

            foreach (var auctionViewModel in VM_Auctions)
            {
                var auction = pageAuctions.FirstOrDefault(a => a.id == auctionViewModel.Id);
                if (auction != null)
                {
                    auctionViewModel.UpdateTimeLeft();
                }
            }


            var visibleAuctions = VM_Auctions
                                    .Where(vm => pageAuctions.Any(a => a.id == vm.Id))
                                    .ToList();

            DisplayedAuctions = new ObservableCollection<VM_AuctionControler>(visibleAuctions);
            UpdateTimeLeftForDisplayedAuctions();
        }
        private void UpdateTimeLeftForDisplayedAuctions()
        {
            if (DisplayedAuctions == null) return;

            foreach (var auction in DisplayedAuctions)
            {
                auction.UpdateTimeLeft();
            }
        }
        public void NextPage()
        {
            if (_currentPage * _itemsPerPage < Auctions.Count)
            {
                _currentPage++;
                UpdateDisplayedAuctions();
            }
        }
        public void PreviousPage()
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                UpdateDisplayedAuctions();
            }
        }

        private void ShowAddAuction()
        {

            var mainWindow = App.Current.Windows
                    .OfType<MainWindow>()
                    .FirstOrDefault();
            var frame = mainWindow?.FindName("MainFrame") as Frame;



            if (frame != null)
            {
                frame.Navigate(new VM_AddAuctionPage());
            }
        }

        private void ReloadAuctions(string sortFilter = "default", string statusFilter = "default")
        {
            _vmAuctions = null;
            SetAuctions(CurrentUser._id);
            UpdateDisplayedAuctions();
        }

        private void OnBack()
        {
            var adminWindow = App.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
            var frame = adminWindow?.FindName("AdminFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new Vm_UsersDetailes());
            }
        }
    }
}