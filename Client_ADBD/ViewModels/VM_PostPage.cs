using Client_ADBD.Models;
using Client_ADBD.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Client_ADBD.Views.UserControl;
using Client_ADBD.Helpers;
using System.Security.Cryptography;
using System.Windows.Threading;

namespace Client_ADBD.ViewModels
{
    internal class VM_PostPage:VM_Base
    {
        private int _postId;

        private Visibility _ownerAdminVisibility;
        private Visibility _imageGridVisibility;
        private Visibility _customerVisibility;

        private int _blurRadius;
        private string[] _imagePaths;
        private string _productName;
        private string _productArtist;
        private decimal _productStartPrice;
        private decimal _productListPrice;
        private string _postStatus;
        private string _selectedImagePath;
        private int _postLotNumber;
        private string _productType;

        private string _lastBidUser;
        private decimal _bidPrice;

        private decimal _lastBidPrice;

        private int _auctionNumber;

        private string _postDescription;
        private bool _isFullDescriptionVisible = false;

        private object _selectedProductControl;

        private DispatcherTimer _timer;
        private DateTime _auctionEndTime;
        private DateTime _auctionStartTime;

        public ICommand CloseImageCommand {  get; set; }    
        public ICommand SelectImageCommand { get; set; }

        public ICommand ToggleDescriptionCommand { get; }
        public ICommand GoBackToAuctionPageCommand { get; set; }

        public ICommand GoToModifyPostPageCommand { get; set; }

        public ICommand DeletePostCommand { get; set; }

        public ICommand AddBidCommand { get; set; }

        public VM_PostPage(Post_ p)
        {

            if (p != null)
            {
                _postId = p.postId;
                _auctionNumber = p.auctionNumber;
                _auctionEndTime=DatabaseManager.GetAuctionByNumber(_auctionNumber).endTime;
                _auctionStartTime=DatabaseManager.GetAuctionByNumber(_auctionNumber).startTime;
                _productName = p.product.Name;
                _lastBidUser = p.lastOfferUser;
                _lastBidPrice=p.lastOffer;
                _productStartPrice=p.product.startPrice;
                _productListPrice = p.product.listPrice;
                _imagePaths = p.product.imagePaths;
                _postStatus=p.productStatus;
                _postDescription=p.product.Description;
                _postLotNumber = p.lotNumber;
                _productType=p.auctionType;

                SelectedProductControl = p.auctionType switch
                {
                    "Tablouri" => new PaintingPostControl(p.product as Client_ADBD.Models.Painting_),
                    "Ceasuri" => new WatchPostControl(p.product as Client_ADBD.Models.Watch_),
                    "Bijuterii" => new JewelryPostControl(p.product as Client_ADBD.Models.Jewelry_),
                    "Carti" => new BookControl(p.product as Client_ADBD.Models.Book_),
                    "Sculpturi" => new SculptureControl(p.product as Client_ADBD.Models.Sculpture_),
                    _ => null,
                };

                _productArtist = p.auctionType switch
                {
                    "Tablouri" => (p.product as Client_ADBD.Models.Painting_).Artist,
                    "Ceasuri" => (p.product as Client_ADBD.Models.Watch_).Manufacturer,
                    "Bijuterii" => (p.product as Client_ADBD.Models.Jewelry_).Brand,
                    "Carti" => (p.product as Client_ADBD.Models.Book_).Author,
                    "Sculpturi" => (p.product as Client_ADBD.Models.Sculpture_).Artist,
                    _ => null,
                };

            }

            _lastBidPriceStr=GetLastOfferText(_lastBidPrice);

            ImageGridVisibility = Visibility.Hidden;
            BlurRadius = 0;

            GoBackToAuctionPageCommand = new RelayCommand(GoBackToAuctionPage);
            CloseImageCommand = new RelayCommand(CloseImage);
            SelectImageCommand = new RelayCommand<string>(SelectImage);
            ToggleDescriptionCommand = new RelayCommand(ToggleDescription);
            GoToModifyPostPageCommand = new RelayCommand(GoToModifyPage);
            DeletePostCommand = new RelayCommand(DeletePost);
            AddBidCommand = new RelayCommand(AddBid);

            if (DatabaseManager.GetPostUser(_auctionNumber) == CurrentUser.User._username)
            {
                OwnerAdminVisibility = Visibility.Visible;
            }
            else
            {
                OwnerAdminVisibility = Visibility.Hidden;
            }

           

           if(DatabaseManager.GetPostUser(_auctionNumber) == CurrentUser.User._username||_auctionEndTime<DateTime.Now||_auctionStartTime>DateTime.Now)
            {
                CustomerVisibility= Visibility.Hidden;
               
            }
            else
            {
                CustomerVisibility = Visibility.Visible;
            }

            Helpers.Timer.AddEventToTimer(UpdateTimeLeft,1,-1);
        }

        private string _status2;
        public string Status2
        {
            get { return _status2; }
            set
            {
                _status2 = value;
                OnPropertyChange(nameof(Status2));
            }
        }



        private string _timeLeft2;
        public string TimeLeft2
        {
            get => _timeLeft2;
            set
            {
                if (_timeLeft2 != value)
                {
                    _timeLeft2 = value;
                    OnPropertyChange(nameof(TimeLeft2));
                }
            }
        }

      
        public string FormatTime()
        {

            TimeSpan timeLeft2 = default;

            if (_auctionStartTime > DateTime.Now)
            {

                timeLeft2 = _auctionStartTime - DateTime.Now;
                Status2= "Upcoming";
            }
            else if (_auctionEndTime > DateTime.Now)
            {
                timeLeft2 = _auctionEndTime - DateTime.Now;
                Status2= "Ongoing";
            }
            else
            {
                Status2 = "Closed";
            }


            int years = timeLeft2.Days / 365;
            int months = (timeLeft2.Days % 365) / 30;
            int days = (timeLeft2.Days % 365) % 30;
            int hours = timeLeft2.Hours;
            int minutes = timeLeft2.Minutes;
            int seconds = timeLeft2.Seconds;

            // Construim un șir formatat pentru a afișa timpul rămas
            string result = "";

            if (years > 0) result += $"{years}y ";
            if (months > 0) result += $"{months}m ";
            if (days > 0) result += $"{days}d ";
            if (hours > 0) result += $"{hours}h ";
            if (minutes > 0) result += $"{minutes}m ";
            if (seconds > 0) result += $"{seconds}s";

            if (string.Compare(Status2, "Closed") == 0)
            {
                return string.Empty;
            }
            else if (string.Compare(Status2, "Ongoing") == 0)
            {
                return string.Concat("Timpul rămas până la șfârșit : ", result.Trim());
            }
            else if (string.Compare(Status2, "Upcoming") == 0)
            {
                return string.Concat("Timpul rămas până la început: ", result.Trim());
            }

            return string.Empty;

        }

        public void UpdateTimeLeft()
        {

            TimeLeft2 = FormatTime();

        }
        private void AddBid()
        {
            if (BidPrice < 0)
            {
                var result = MessageBox.Show(
                     "Suma introdusa este incorectă.",
                     "Eroare ofertă",
                     MessageBoxButton.OK,
                     MessageBoxImage.Warning);
                return;
            }

            if(BidPrice > CurrentUser.User._balance)
            {
              var result = MessageBox.Show(
                          "Sold insuficient.",
                          "Eroare ofertă",
                          MessageBoxButton.OK,
                          MessageBoxImage.Warning);
                return;
            }else if(BidPrice <= _lastBidPrice) 
            {
                var result = MessageBox.Show(
                           "Ofertă trebuie să fie mai mare decât cea curentă.",
                           "Eroare ofertă",
                           MessageBoxButton.OK,
                           MessageBoxImage.Warning);

               
                return;
            }
            else
            {
                DatabaseManager.AddBid(_postId, CurrentUser.User._id, BidPrice);
                _lastBidPrice = DatabaseManager.GetPostLastOffer(_postId, ref _lastBidUser);
                LastBidPrice= GetLastOfferText(_lastBidPrice); 
            }
        }
        public decimal BidPrice
        {
            get => _bidPrice;
            set
            {
                _bidPrice = value;
                OnPropertyChange(nameof(BidPrice));
            }
        }

        private string GetLastOfferText(decimal lastOffer)
        {
            if(lastOffer<0)
                return string.Empty;

            return "Ultima ofertă: " + lastOffer.ToString("F2") + " $";
        }

        private string _lastBidPriceStr;
        public string LastBidPrice
        {
            get => _lastBidPriceStr;
            set
            {
                _lastBidPriceStr = value;
                OnPropertyChange(nameof(LastBidPrice));
            }
            
        }
   
        private void DeletePost()
        {

            var result = MessageBox.Show(
                     "Sunteți sigur că doriți să ștergeți această postare?",
                     "Confirmare ștergere",
                     MessageBoxButton.OKCancel,
                     MessageBoxImage.Warning);

            if (result == MessageBoxResult.OK)
            {
                var rc = false;
                rc = DatabaseManager.DeletePost(_postId,_productType);

                if (rc == false)
                {
                    GoBackToAuctionPage();
                    MessageBox.Show("Postare ștearsă cu succes!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Postarea nu a putut fi ștearsă!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

        }

        public Visibility CustomerVisibility
        {
            get => _customerVisibility;
            set
            {
                _customerVisibility = value;
                OnPropertyChange(nameof(CustomerVisibility));
            }
        }
        private void GoToModifyPage()
        {
       
            Post_ p = DatabaseManager.GetPostDetails(_postId);

            var mainWindow = App.Current.Windows
                     .OfType<MainWindow>()
                     .FirstOrDefault();
            var frame = mainWindow?.FindName("MainFrame") as Frame;


            if (frame != null)
            {
                frame.Navigate(new ModifyPostPage(p));
            }

        }

        public int PostLotNumber
        {
            get { return _postLotNumber; }
            set
            {
                _postLotNumber = value;
                OnPropertyChange(nameof(PostLotNumber));
            }
        }

        public object SelectedProductControl
        {
            get => _selectedProductControl;
            set
            {
                _selectedProductControl = value;
                OnPropertyChange(nameof(SelectedProductControl));
            }
        }

        public VM_PostPage() { }


        //public string ButtonText => IsFullDescriptionVisible ? "Ascunde" : "Citește mai mult";

        public bool IsFullDescriptionVisible
        {
            get => _isFullDescriptionVisible;
            set
            {
                if (_isFullDescriptionVisible != value)
                {
                    _isFullDescriptionVisible = value;
                    OnPropertyChange(nameof(IsFullDescriptionVisible));
                    OnPropertyChange(nameof(ButtonText)); // Actualizează textul butonului
                    OnPropertyChange(nameof(DescriptionText)); // Actualizează descrierea
                }
            }
        }

        public string PostDescription
        {
            get => _postDescription;
            set
            {
                if (_postDescription != value)
                {
                    _postDescription = value;
                    OnPropertyChange(nameof(PostDescription));
                    OnPropertyChange(nameof(ShortDescription));
                    OnPropertyChange(nameof(FullDescription));
                }
            }
        }

        public string FullDescription => PostDescription;
        public string ShortDescription => GetFirstNWords(PostDescription, 50);
        private string GetFirstNWords(string description, int numberOfWords)
        {
            if (description == null)
            {
                return string.Empty;
            }

            var words = description.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", words.Take(numberOfWords)) + " ...";

        }

        public string ButtonText
        {
            get
            {
                if(string.IsNullOrEmpty(ShortDescription)||string.IsNullOrEmpty(FullDescription))
                {
                    return string.Empty;
                }
                if (FullDescription.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length <= 50)
                {
                    return string.Empty;
                }
                return IsFullDescriptionVisible ? "Ascunde" : "Citește mai mult";
            }
        }

        public string DescriptionText => IsFullDescriptionVisible ? FullDescription : ShortDescription;

        private void ToggleDescription()
        {
            IsFullDescriptionVisible = !IsFullDescriptionVisible;
        }

        public string SelectedImagePath
        {
            get { return _selectedImagePath; } 
            set { 
                _selectedImagePath = value;
                OnPropertyChange(nameof(SelectedImagePath));    
            }
        }
        public int AuctionNumber
        {
            get => _auctionNumber;
            set
            {
                _auctionNumber = value;
                OnPropertyChange(nameof(AuctionNumber));
            }

        }


        private void SelectImage(string imagePath)
        {
            SelectedImagePath= imagePath;
            ImageGridVisibility = Visibility.Visible;
            BlurRadius = 20;
        }

       private void CloseImage()
       {
            ImageGridVisibility = Visibility.Hidden;
            BlurRadius = 0;
       }

        public string PostStatus
        {

            get
            {
                {
                    if (_postStatus == "adjudecat")
                    {
                        return $"{_postStatus}: {ProductListPrice} $";
                    }
                    return _postStatus;
                }
            }
            set
            {
                _postStatus = value;
                OnPropertyChange(nameof(PostStatus));
            }
        }

        public decimal ProductListPrice
        {
            get { return _productListPrice; }
            set
            {
                _productListPrice = value;
                OnPropertyChange(nameof(ProductListPrice));
            }
        }

        public Visibility OwnerAdminVisibility
        {
            get => _ownerAdminVisibility;
            set
            {
                _ownerAdminVisibility = value;
                OnPropertyChange(nameof(OwnerAdminVisibility));
            }
        }
        public Visibility ImageGridVisibility
        {
            get => _imageGridVisibility;
            set
            {
                if (_imageGridVisibility != value)
                {
                    _imageGridVisibility = value;
                    OnPropertyChange(nameof(ImageGridVisibility));
                }
            }
        }


        public int BlurRadius
        {
            get => _blurRadius;
            set
            {
                _blurRadius = value;
                OnPropertyChange(nameof(BlurRadius));
            }
        }

        public decimal ProductStartPrice
        {
            get => _productStartPrice;
            set
            {

                _productStartPrice = value;
                OnPropertyChange(nameof(ProductStartPrice));
            }
        }

        public string ProductArtist
        {
            get => _productArtist;
            set {
                _productArtist = value;
                OnPropertyChange(nameof(ProductArtist));
            }
        }

        public string ProductName
        {
            get => _productName;
            set
            {

                _productName = value;
                OnPropertyChange(nameof(ProductName));
            }
        }

        public string[] ImagePaths
        {
            get { return _imagePaths;  }
            set
            {
                if (_imagePaths != value)
                {
                    _imagePaths = value;
                    OnPropertyChange(nameof(ImagePaths));
                }
            }
        }

        private void GoBackToAuctionPage() 
        {

            var mainWindow = App.Current.Windows
             .OfType<MainWindow>()
             .FirstOrDefault();
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            Auction_ a = DatabaseManager.GetAuctionByNumber(_auctionNumber);

            if (frame != null)
            {
                frame.Navigate(new AuctionPage(a));
            }
        }

        

    }
}
