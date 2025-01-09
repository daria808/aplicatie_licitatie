using Client_ADBD.Models;
using Client_ADBD.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client_ADBD.ViewModels
{
    internal class VM_MainAdminPage : VM_Base
    {
        private int _totalUsers;
        private int _totalBidders;
        private int _totalClients;
        private int _totalActiveAuctions;

        public int TotalUsers
        {
            get => _totalUsers;
            set
            {
                _totalUsers = value;
                OnPropertyChange(nameof(TotalUsers));
            }
        }

        public int TotalBidders
        {
            get => _totalBidders;
            set
            {
                _totalBidders = value;
                OnPropertyChange(nameof(TotalBidders));
            }
        }

        public int TotalClients
        {
            get => _totalClients;
            set
            {
                _totalClients = value;
                OnPropertyChange(nameof(TotalClients));
            }
        }

        public int TotalActiveAuctions
        {
            get => _totalActiveAuctions;
            set
            {
                _totalActiveAuctions = value;
                OnPropertyChange(nameof(TotalActiveAuctions));
            }
        }

        public string _sortFilter;
        public ICommand NextPageCommand { get; set; }
        public ICommand PreviousPageCommad { get; set; }
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

        public VM_MainAdminPage(string sortFilter = "default", string statusFilter = "default")
        {
            LoadStatistics();

            SelectedSortOption = sortFilter;
            Status = statusFilter;
            NextPageCommand = new RelayCommand(NextPage);
            PreviousPageCommad = new RelayCommand(PreviousPage);

            Helpers.Utilities.OnStatusChanged += (sender, e) =>
            {
                Status = Helpers.Utilities.Status;
            };

            Helpers.Timer.AddEventToTimer(UpdatePostStatus, -1, 5);
            Helpers.Timer.AddEventToTimer(UpdateTimeLeftForDisplayedAuctions, 1, -1);

        }

        private void LoadStatistics()
        {
            TotalUsers = (new DatabaseManager()).CountTotalUsers();
            TotalBidders = (new DatabaseManager()).CountTotalBidders();
            TotalClients = (new DatabaseManager()).CountTotalClients();
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

        public void SetAuctions(List<Auction_> auctions)
        {
            if (_vmAuctions == null || DisplayedAuctions.Count() == 0)
            {
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

        private void ReloadAuctions(string sortFilter = "default", string statusFilter = "default")
        {
            _vmAuctions = null;
            SetAuctions(DatabaseManager.GetAuction(statusFilter, sortFilter));
            UpdateDisplayedAuctions();
        }
    }
}