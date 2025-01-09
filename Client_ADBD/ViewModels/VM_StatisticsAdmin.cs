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
    internal class VM_StatisticsAdmin : VM_Base
    {


        public string _sortFilter;

        public ICommand BackCommand { get; set; }
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommad { get; }

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
        public VM_StatisticsAdmin(string sortFilter = "default", string statusFilter = "Closed")
        {
            SelectedSortOption = sortFilter;
            Status = statusFilter;

            BackCommand = new RelayCommand(OnBackPressed);

            NextPageCommand = new RelayCommand(NextPage);
            PreviousPageCommad = new RelayCommand(PreviousPage);

            Helpers.Utilities.OnStatusChanged += (sender, e) =>
            {
                Status = Helpers.Utilities.Status;
            };

            Helpers.Timer.AddEventToTimer(UpdatePostStatus, -1, 5);
        }


        private void OnBackPressed()
        {
            var adminWindow = App.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
            var frame = adminWindow?.FindName("AdminFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_MainAdminPage());
            }
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

        private ObservableCollection<VM_StatisticsControler> _vmAuctions;
        public ObservableCollection<VM_StatisticsControler> VM_Auctions
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



        private ObservableCollection<VM_StatisticsControler> _displayedAuctions;
        public ObservableCollection<VM_StatisticsControler> DisplayedAuctions
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





        public void SetAuctions(List<Auction_> auctions)
        {
            // Filtrare postări cu status "closed"
            var closedAuctions = auctions.Where(a => a.endTime < DateTime.Now).ToList();

            if (_vmAuctions == null || DisplayedAuctions.Count() == 0)
            {
                _vmAuctions = new ObservableCollection<VM_StatisticsControler>(
                     closedAuctions.Select(a => new VM_StatisticsControler(a.id)
                     {
                         Id = a.id,
                         Name = a.name,
                         EndTime = a.endTime,
                         Location = a.location,
                         ImagePath = a.imagePath
                     })
                );


            }

            Auctions = new ObservableCollection<Auction_>(closedAuctions);

            _currentPage = 1;
            UpdateDisplayedAuctions(true);
        }

        private void UpdateDisplayedAuctions(bool admin = false)
        {
            if (Auctions == null || VM_Auctions == null) return;

            var pageAuctions = Auctions.Skip((_currentPage - 1) * _itemsPerPage).Take(_itemsPerPage);




            var visibleAuctions = VM_Auctions
                                    .Where(vm => pageAuctions.Any(a => a.id == vm.Id))
                                    .ToList();
            DisplayedAuctions = new ObservableCollection<VM_StatisticsControler>(visibleAuctions);


        }


        public void NextPage()
        {
            if (_currentPage * _itemsPerPage < Auctions.Count)
            {
                _currentPage++;
                UpdateDisplayedAuctions(true);
            }
        }
        public void PreviousPage()
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                UpdateDisplayedAuctions(true);
            }
        }


        private void ReloadAuctions(string sortFilter = "default", string statusFilter = "Closed")
        {
            _vmAuctions = null;
            SetAuctions(DatabaseManager.GetAuction(statusFilter, sortFilter));
            UpdateDisplayedAuctions(true);
        }
    }
}