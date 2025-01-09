using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client_ADBD.Helpers;
using System.Windows.Controls;
using System.Windows.Input;
using Client_ADBD.Models;
using Client_ADBD.Views;
using CommunityToolkit.Mvvm.Input;

namespace Client_ADBD.ViewModels
{
    internal class VM_ModifyAuctionPage:VM_Base
    {
        private Auction_ _initialAuction {  get; set; }
        private bool Admin;
        public VM_ModifyAuctionPage() { }
        public VM_ModifyAuctionPage(Auction_ a, bool isAdmin) {
            Admin = isAdmin;
            _initialAuction = a;

            if (Admin == true)
            {
                ClosePageCommand = new RelayCommand(ClosePageAdmin);
            }
            else
            {
                ClosePageCommand = new RelayCommand(ClosePage);
            }
            SaveChangesCommand = new RelayCommand(SaveChanges);
            AuctionName =a.name;
            StartDate = a.startTime;
            StartHour = a.startTime.ToString("HH");
            StartMinute = a.startTime.ToString("mm");

            EndDate = a.endTime;
            EndHour = a.endTime.ToString("HH");
            EndMinute = a.endTime.ToString("mm");

            ImagePath = a.imagePath;
            Description = a.description;
            Location = a.location;

        }

        private bool isValid = true;
        bool isModified = false;
        private string _auctionName;
        //private string _auctionType;
        private DateTime _startDate = DateTime.Now;
        private string _startHour;
        private string _startMinute;
        private DateTime _endDate = DateTime.Now;
        private string _endHour;
        private string _endMinute;
        private string _imagePath;
        private string _description;
        private string _location;

        public string AuctionName
        {
            get { return _auctionName; }
            set
            {
                if (_auctionName != value)
                {
                    _auctionName = value;
                    AuctionNameError = Helpers.validation.IsValidAuctionName(_auctionName);
                    OnPropertyChange(nameof(AuctionName));
                }
            }
        }
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    StartDateError = Helpers.validation.IsValidStartDate(_startDate, ref isValid);
                    OnPropertyChange(nameof(StartDate));
                }
            }
        }
        public string StartHour
        {
            get { return _startHour; }
            set
            {
                if (_startHour != value)
                {
                    _startHour = value;
                    OnPropertyChange(nameof(StartHour));
                }
            }
        }
        public string StartMinute
        {
            get { return _startMinute; }
            set
            {
                if (_startMinute != value)
                {
                    _startMinute = value;
                    OnPropertyChange(nameof(StartMinute));
                }
            }
        }
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    EndDateError = Helpers.validation.IsValidEndDate(_endDate, ref isValid);
                    OnPropertyChange(nameof(EndDate));
                }
            }
        }
        public string EndHour
        {
            get { return _endHour; }
            set
            {
                if (_endHour != value)
                {
                    _endHour = value;
                    OnPropertyChange(nameof(EndHour));
                }
            }
        }
        public string EndMinute
        {
            get { return _endMinute; }
            set
            {
                if (_endMinute != value)
                {
                    _endMinute = value;
                    OnPropertyChange(nameof(EndMinute));
                }
            }
        }
        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                    ImagePathError = Helpers.validation.IsValidImagePath(ImagePath);
                    OnPropertyChange(nameof(ImagePath));
                }
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    DescriptionError = Helpers.validation.IsValidDescription(Description);
                    OnPropertyChange(nameof(Description));
                }
            }
        }

        public string Location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    LocationError = Helpers.validation.IsValidLocation(Location);
                    OnPropertyChange(nameof(Location));
                }
            }
        }
        public ICommand ClosePageCommand { get; }
        public ICommand SaveChangesCommand { get; }
       

        private void ClosePage()
        {

            var mainWindow = App.Current.Windows
                     .OfType<MainWindow>()
                     .FirstOrDefault();
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            var a = DatabaseManager.GetAuctionByNumber(_initialAuction.auctionNumber);

            if (frame != null)
            {
                frame.Navigate(new AuctionPage(a));
            }

        }

        private void ClosePageAdmin()
        {
            var a = DatabaseManager.GetAuctionByNumber(_initialAuction.auctionNumber);


            var adminWindow = App.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
            var frame = adminWindow?.FindName("AdminFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new AuctionPage(a, false, false, false, true));
            }
        }

        private string _auctionNameError;
        public string AuctionNameError
        {
            get => _auctionNameError;
            set
            {
                _auctionNameError = value;
                OnPropertyChange(nameof(AuctionNameError));
            }
        }

        private string _auctionTypeError;
        public string AuctionTypeError
        {
            get => _auctionTypeError;
            set
            {
                _auctionTypeError = value;
                OnPropertyChange(nameof(AuctionTypeError));
            }
        }

        private string _startDateError;
        public string StartDateError
        {
            get => _startDateError;
            set
            {
                _startDateError = value;
                OnPropertyChange(nameof(StartDateError));
            }
        }

        private string _endDateError;
        public string EndDateError
        {
            get => _endDateError;
            set
            {
                _endDateError = value;
                OnPropertyChange(nameof(EndDateError));
            }
        }

        private string _timeError;
        public string TimeError
        {
            get => _timeError;
            set
            {
                _timeError = value;
                OnPropertyChange(nameof(TimeError));
            }
        }

        private string _imagePathError;

        public string ImagePathError
        {
            get => _imagePathError;
            set
            {
                _imagePathError = value;
                OnPropertyChange(nameof(ImagePathError));
            }
        }

        private string _locationError;

        public string LocationError
        {
            get => _locationError;
            set
            {
                _locationError = value;
                OnPropertyChange(nameof(LocationError));
            }
        }

        private string _descriptionError;
        public string DescriptionError
        {
            get => _descriptionError;
            set
            {
                _descriptionError = value;
                OnPropertyChange(nameof(DescriptionError));
            }
        }

        private void SaveChanges()
        {
            AuctionNameError = string.Empty;
            AuctionTypeError = string.Empty;
            StartDateError = string.Empty;
            EndDateError = string.Empty;
            TimeError = string.Empty;

            ImagePathError = string.Empty;
            LocationError = string.Empty;
            DescriptionError = string.Empty;

            bool isValid = true;

            AuctionNameError = Helpers.validation.IsValidAuctionName(AuctionName);
            StartDateError = Helpers.validation.IsValidStartDate(StartDate, ref isValid);
            EndDateError = Helpers.validation.IsValidEndDate(EndDate, ref isValid);
            ImagePathError = Helpers.validation.IsValidImagePath(ImagePath);
            LocationError = Helpers.validation.IsValidLocation(Location);
            DescriptionError = Helpers.validation.IsValidDescription(Description);

            DateTime startDateTime = default(DateTime);
            DateTime endDateTime = default(DateTime);

            TimeError = Helpers.validation.IsValidTime(StartDate, StartHour, StartMinute, EndDate, EndHour, EndMinute, ref startDateTime, ref endDateTime, ref isValid);



            if (Helpers.validation.IsValidAuction(AuctionNameError, AuctionTypeError, StartDateError, EndDateError, TimeError, ImagePathError, DescriptionError, LocationError))
            {

                if (AuctionName != _initialAuction.name ||
                    StartDate != _initialAuction.startTime.Date ||
                    StartHour != _initialAuction.startTime.ToString("HH")||
                    StartMinute !=_initialAuction.startTime.ToString("mm")||
                    EndDate != _initialAuction.endTime.Date ||
                    EndHour != _initialAuction.endTime.ToString("HH") ||
                    EndMinute != _initialAuction.endTime.ToString("mm") ||
                    ImagePath != _initialAuction.imagePath ||
                    Description != _initialAuction.description ||
                    Location != _initialAuction.location )
                {
                
                    isModified = true;
                }

                if (isModified)
                {
                    startDateTime = new DateTime(
                              startDateTime.Year,   // Păstrează anul din startDateTime
                              startDateTime.Month,  // Păstrează luna din startDateTime
                              startDateTime.Day,    // Păstrează ziua din startDateTime
                              int.Parse(StartHour), // Ora din StartHour
                              int.Parse(StartMinute), // Minutul din StartMinute
                              0                      // Păstrează secunda 0
                          );

                    endDateTime = new DateTime(
                              endDateTime.Year,   // Păstrează anul din startDateTime
                              endDateTime.Month,  // Păstrează luna din startDateTime
                              endDateTime.Day,    // Păstrează ziua din startDateTime
                              int.Parse(EndHour), // Ora din StartHour
                              int.Parse(EndMinute), // Minutul din StartMinute
                              0                      // Păstrează secunda 0
                          );

                    Auction_ auctionToUpdate = new Auction_
                    {
                        auctionNumber = _initialAuction.auctionNumber,  // Păstrează numărul licitației neschimbat
                        name = AuctionName != _initialAuction.name ? AuctionName : _initialAuction.name,
                        startTime = startDateTime != _initialAuction.startTime ? startDateTime : _initialAuction.startTime,
                        endTime = endDateTime != _initialAuction.endTime ? endDateTime : _initialAuction.endTime,
                        imagePath = ImagePath != _initialAuction.imagePath ? ImagePath : _initialAuction.imagePath,
                        description = Description != _initialAuction.description ? Description : _initialAuction.description,
                        location = Location != _initialAuction.location ? Location : _initialAuction.location
                    };

                    DatabaseManager.UpdateAuction(auctionToUpdate);
                }

                    Auction_ new_auction = DatabaseManager.GetAuctionByNumber(_initialAuction.auctionNumber);
                    ShowAuctionPage(new_auction);
            }


        }
        private void ShowAuctionPage(Auction_ new_auction)
        {
            //var mainWindow = App.Current.MainWindow as MainWindow;
            //var frame = mainWindow?.FindName("MainFrame") as Frame;

            //if (frame != null)
            //{
            //    frame.Navigate(new VM_AuctionPage(new_auction));
            //}

            if (Admin == true)
            {
                var adminWindow = App.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
                var frame = adminWindow?.FindName("AdminFrame") as Frame;

                if (frame != null)
                {
                    frame.Navigate(new AuctionPage(new_auction, true));
                }
            }
            else
            {
                var mainWindow = App.Current.Windows
                        .OfType<MainWindow>()
                        .FirstOrDefault();
                var frame = mainWindow?.FindName("MainFrame") as Frame;

                if (frame != null)
                {
                    frame.Navigate(new AuctionPage(new_auction));
                }
            }
        }
    }
}
