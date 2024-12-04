using Client_ADBD.Models;
using System;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Client_ADBD.ViewModels
{
    internal class VM_MainPage:VM_Base
    {
        private DispatcherTimer _timer;
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommad { get; }

        private object _selectedViewModel;
        public object SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                OnPropertyChange(nameof(SelectedViewModel));
            }
        }

        public VM_MainPage()
        {
            NextPageCommand = new RelayCommand(NextPage);
            PreviousPageCommad = new RelayCommand(PreviousPage);

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += (s, e) => UpdateTimeLeftForDisplayedAuctions();
            _timer.Start();

        }

        private int _currentPage = 1;
        private int _itemsPerPage = 4;

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
            if (DisplayedAuctions == null || DisplayedAuctions.Count() == 0)
            {
                var auctionViewModels = auctions.Select(a => new VM_AuctionControler
                {

                    Name = a.name,
                    StartTime = a.startTime,
                    EndTime = a.endTime,
                    Location = a.location,
                    Status = a.statusStr

                }).ToList();

                foreach (var auctionViewModel in auctionViewModels)
                {
                    auctionViewModel.UpdateTimeLeft();
                }

                DisplayedAuctions = new ObservableCollection<VM_AuctionControler>(auctionViewModels);
            }


            Auctions = new ObservableCollection<Auction_>(auctions);


            _currentPage = 1;
            UpdateDisplayedAuctions();
        }
        private void UpdateDisplayedAuctions()
        {
            if (Auctions == null || DisplayedAuctions == null) return;

            var pageAuctions = Auctions.Skip((_currentPage - 1) * _itemsPerPage).Take(_itemsPerPage);

            foreach (var auctionViewModel in DisplayedAuctions)
            {
                var auction = pageAuctions.FirstOrDefault(a => a.name == auctionViewModel.Name);
                if (auction != null)
                {
                    auctionViewModel.UpdateTimeLeft();
                }
            }


            var visibleAuctions = DisplayedAuctions
                                    .Where(vm => pageAuctions.Any(a => a.name == vm.Name))
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
    }
}
