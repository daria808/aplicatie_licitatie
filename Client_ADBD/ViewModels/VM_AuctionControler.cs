using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using Client_ADBD.Helpers;
using Client_ADBD.Views;
using System.Windows.Controls;

namespace Client_ADBD.ViewModels
{
    internal class VM_AuctionControler:VM_Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public int Number {  get; set; }    

        private string _timeLeft;
        public ICommand NavigateToAuctionDetailsCommand {  get; }

        public VM_AuctionControler() 
        {
            NavigateToAuctionDetailsCommand = new RelayCommand(GoToAuctionPage);
        }

        /// <summary>
        /// MODIFICAT!!!
        /// </summary>
        private void GoToAuctionPage()
        {
            //NavigationService.OpenWindow("ErrorWindow","Postare");

            //var a=DatabaseManager.GetAuctionByName(Name);
            var a = DatabaseManager.GetAuctionByNumber(Number);

            var mainWindow = App.Current.Windows
                     .OfType<MainWindow>()
                     .FirstOrDefault();
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new AuctionPage(a));
            }

        }

        public string TimeLeft
        {
            get => _timeLeft;
            set
            {
                if (_timeLeft != value)
                {
                    _timeLeft = value;
                    OnPropertyChange(nameof(TimeLeft));
                }
            }
        }
        public string FormatTime()
        {

            TimeSpan timeLeft=default;

            if (StartTime > DateTime.Now)
            {

                timeLeft = StartTime - DateTime.Now;
                Status = "Upcoming";
            }
            else if (EndTime > DateTime.Now)
            {
                timeLeft = EndTime - DateTime.Now;
                Status = "Ongoing";
            }
            else
            {
                Status = "Closed";
            }
               

            int years = timeLeft.Days / 365;
            int months = (timeLeft.Days % 365) / 30;
            int days = (timeLeft.Days % 365) % 30;
            int hours = timeLeft.Hours;
            int minutes = timeLeft.Minutes;
            int seconds = timeLeft.Seconds;

            // Construim un șir formatat pentru a afișa timpul rămas
            string result = "";

            if (years > 0) result += $"{years}y ";
            if (months > 0) result += $"{months}m ";
            if (days > 0) result += $"{days}d ";
            if (hours > 0) result += $"{hours}h ";
            if (minutes > 0) result += $"{minutes}m ";
            if (seconds > 0) result += $"{seconds}s";

            if(string.Compare(Status,"Closed")==0)
            { 
                return string.Empty;
            }
            else if(string.Compare(Status, "Ongoing") == 0)
            {
                return string.Concat("Timpul rămas până la șfârșit : ", result.Trim());
            }
            else if(string.Compare(Status,"Upcoming")==0)
            {
                return string.Concat("Timpul rămas până la început: ", result.Trim());
            }
            
            return string.Empty;

        }

        public void UpdateTimeLeft()
        {

            TimeLeft = FormatTime();

        }

        //adaugat
        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                OnPropertyChange(nameof(ImagePath));
            }
        }
    }
}
