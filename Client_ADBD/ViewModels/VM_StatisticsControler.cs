using Client_ADBD.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client_ADBD.ViewModels
{
    internal class VM_StatisticsControler : VM_Base
    {
        public int Id;
        private DateTime _endTime;
        private string _imagePath;
        private string _name;
        private string _location;

        public decimal Total {  get; set; }
        public double Procent { get; set; }

        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                _endTime = value;
                OnPropertyChange(nameof(EndTime));
            }
        }

        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                OnPropertyChange(nameof(ImagePath));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChange(nameof(Name));
            }
        }

        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChange(nameof(Location));
            }
        }

        public ICommand NavigateToAuctionDetailsCommand { get; }
        public VM_StatisticsControler(int id, bool admin = false)
        {
            Id = id;
            if (admin == true)
            {
                NavigateToAuctionDetailsCommand = new RelayCommand(GoToAuctionPageAdmin);
            }
            else
            {
                NavigateToAuctionDetailsCommand = new RelayCommand(GoToAuctionPage);
            }

            // var databaseManager = new DatabaseManager();
            Total = DatabaseManager.GetTotalBidsForAuction(id);
            Procent = DatabaseManager.GetSoldPercentage(id);
        }

        private void GoToAuctionPage()
        {
            //NavigationService.OpenWindow("ErrorWindow","Postare");

            //var a=DatabaseManager.GetAuctionByName(Name);
            var a = DatabaseManager.GetAuctionByName(Name);

            var mainWindow = App.Current.Windows
                     .OfType<MainWindow>()
                     .FirstOrDefault();
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new AuctionPage(a, true, true));
            }

        }

        private void GoToAuctionPageAdmin()
        {
            //NavigationService.OpenWindow("ErrorWindow","Postare");

            //var a=DatabaseManager.GetAuctionByName(Name);
            var a = DatabaseManager.GetAuctionByName(Name);

            var adminWindow = App.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
            var frame = adminWindow?.FindName("AdminFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_AuctionPage(a, true, false, true));
            }

        }
    }
}
