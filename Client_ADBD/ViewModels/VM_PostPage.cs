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

namespace Client_ADBD.ViewModels
{
    internal class VM_PostPage:VM_Base
    {
        private int _postId;

        private Visibility _imageGridVisibility;
        private int _blurRadius;
        private string[] _imagePaths;
        private string _productName;
        private string _productArtist;
        private decimal _productStartPrice;
        private decimal _productListPrice;
        private string _postStatus;
        private string _selectedImagePath;
        private int _postLotNumber;



        private int _auctionNumber;

        private string _postDescription;
        private bool _isFullDescriptionVisible = false;

        private object _selectedProductControl;

        public ICommand CloseImageCommand {  get; set; }    
        public ICommand SelectImageCommand { get; set; }

        public ICommand ToggleDescriptionCommand { get; }
        public VM_PostPage(Post_ p)
        {

            if (p != null)
            {
                _postId = p.postId;
                _auctionNumber = p.auctionNumber;
                _productName = p.product.Name;
                _productStartPrice=p.product.startPrice;
                _productListPrice = p.product.listPrice;
                _imagePaths = p.product.imagePaths;
                _postStatus=p.productStatus;
                _postDescription=p.product.Description;
                _postLotNumber = p.lotNumber;

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

            ImageGridVisibility = Visibility.Hidden;
            BlurRadius = 0;
            GoBackToAuctionPageCommand = new RelayCommand(GoBackToAuctionPage);
            CloseImageCommand = new RelayCommand(CloseImage);
            SelectImageCommand = new RelayCommand<string>(SelectImage);
            ToggleDescriptionCommand = new RelayCommand(ToggleDescription);
 
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


        public ICommand GoBackToAuctionPageCommand {  get; set; }

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
            get { return _imagePaths; }
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
