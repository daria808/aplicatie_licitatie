using Client_ADBD.Models;
using Client_ADBD.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using Client_ADBD.Helpers;

namespace Client_ADBD.ViewModels
{
    internal class VM_AddAuctionPage : VM_Base
    {

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

        private bool isValid = true;
        private string _auctionName;
        private string _auctionType;
        private DateTime _startDate= DateTime.Now;
        private string _startHour;
        private string _startMinute;
        private DateTime _endDate= DateTime.Now;
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
                    AuctionNameError=Helpers.validation.IsValidAuctionName(_auctionName);
                    OnPropertyChange(nameof(AuctionName));
                }
            }
        }
        public string AuctionType
        {
            get { return _auctionType; }
            set
            {
                if (_auctionType != value)
                {
                    _auctionType = value;
                    AuctionTypeError=Helpers.validation.IsValidAuctionType(_auctionType);
                    OnPropertyChange(nameof(AuctionType));
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
                    StartDateError=Helpers.validation.IsValidStartDate(_startDate,ref isValid);
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
                    EndDateError=Helpers.validation.IsValidEndDate(_endDate, ref isValid);
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
                    ImagePathError=Helpers.validation.IsValidImagePath(ImagePath);
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
                    DescriptionError=Helpers.validation.IsValidDescription(Description);
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
                    LocationError=Helpers.validation.IsValidLocation(Location);
                    OnPropertyChange(nameof(Location));
                }
            }
        }
        public ICommand ClosePageCommand { get; }
        public ICommand AddAuctionCommand { get; }
        public VM_AddAuctionPage()
        {
            ClosePageCommand = new RelayCommand(ClosePage);
            AddAuctionCommand = new RelayCommand(AddAuction);
        }

        private void ClosePage()
        {
  
            var mainWindow = App.Current.Windows
                     .OfType<MainWindow>()
                     .FirstOrDefault();
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_MainPage());
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

        private void AddAuction()
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

            AuctionNameError =Helpers.validation.IsValidAuctionName(AuctionName);
            AuctionTypeError=Helpers.validation.IsValidAuctionType(AuctionType);
            StartDateError = Helpers.validation.IsValidStartDate(StartDate,ref isValid);
            EndDateError = Helpers.validation.IsValidEndDate(EndDate, ref isValid);
            ImagePathError=Helpers.validation.IsValidImagePath(ImagePath);
            LocationError=Helpers.validation.IsValidLocation(Location);
            DescriptionError=Helpers.validation.IsValidDescription(Description);

            DateTime startDateTime = default(DateTime);
            DateTime endDateTime=default(DateTime);

            TimeError = Helpers.validation.IsValidTime(StartDate, StartHour, StartMinute, EndDate, EndHour, EndMinute,ref startDateTime,ref endDateTime, ref isValid);

            if (Helpers.validation.IsValidAuction(AuctionNameError, AuctionTypeError, StartDateError, EndDateError, TimeError,ImagePathError,DescriptionError,LocationError))
            {
                if(AuctionType=="Cărți")
                {
                    AuctionType = "Carti";
                }

                DatabaseManager.AddAuction(AuctionName, AuctionType, startDateTime, endDateTime, ImagePath, Description, Location, CurrentUser.User._username /*Helpers.Utilities.Username*/);
                Auction_ new_auction=DatabaseManager.GetAuctionByName(AuctionName);
                ShowAuctionPage(new_auction);
            }
  

        }
        private void ShowAuctionPage(Auction_ new_auction)
        {
            //var mainWindow = App.Current.MainWindow as MainWindow;
            var mainWindow = App.Current.Windows
                     .OfType<MainWindow>()
                     .FirstOrDefault();
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_AuctionPage(new_auction));
            }
        }
    }
    }
