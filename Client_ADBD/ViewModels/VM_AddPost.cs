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
    internal class VM_AddPost:VM_Base
    {
        private object _selectedProductControl;
        private int _auctionNumber {  get; set; }
        private string _productName {  get; set; }
        private decimal _startPrice {  get; set; }
        private decimal _listPrice {  get; set; }

        private string[] _imagePaths = new string[3];
        private string _description {   get; set; }
        private DateTime _invDate { get; set; }=DateTime.Now;

        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                ProductNameError=Helpers.validation.IsValidProducttName(value);
                OnPropertyChange(nameof(ProductName));
            }
        }

        public decimal StartPrice
        {
            get => _startPrice;
            set
            {
                _startPrice = value;
                StartPriceError=Helpers.validation.IsValidStartPrice(value);
                OnPropertyChange(nameof(StartPrice));
            }
        }

        public decimal ListPrice
        {
            get => _listPrice;
            set
            {
                _listPrice = value;
                ListPriceError=Helpers.validation.IsValidListPrice(value);
                OnPropertyChange(nameof(ListPrice));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                DescriptionError=Helpers.validation.IsValidDescription(value);
                OnPropertyChange(nameof(Description));
            }
        }

        public string[] ImagePaths
        {
            get => _imagePaths;
            set
            {
                _imagePaths = value;
                ImagePathError=Helpers.validation.AreValidImagePaths(value);
                OnPropertyChange(nameof(ImagePaths));
            }
        }

        public DateTime InvDate
        {
            get => _invDate;
            set
            {
                _invDate = value;
                InvDateError=Helpers.validation.IsValidInvDate(value);
                OnPropertyChange(nameof(InvDate));
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

        public string ProductType { get; }
        public ICommand ClosePageCommand { get; set; }  
        public ICommand AddPostCommand { get; set; }

        public VM_AddPost(string productType,int auctionNumber)
        { 

            ProductType = productType;
            this._auctionNumber=auctionNumber;
            UpdateSelectedControl();
            ClosePageCommand=new RelayCommand(ClosePage);
            AddPostCommand = new RelayCommand(AddPost);
        }

        private string _productNameError;
        public string ProductNameError
        {
            get => _productNameError;
            set
            {
                _productNameError = value;
                OnPropertyChange(nameof(ProductNameError));
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

        private string _listPriceError;
        public string ListPriceError
        {
            get => _listPriceError;
            set
            {
                _listPriceError = value;
                OnPropertyChange(nameof(ListPriceError));
            }
        }

        private string _startPriceError;
        public string StartPriceError
        {
            get => _startPriceError;
            set
            {
                _startPriceError = value;
                OnPropertyChange(nameof(StartPriceError));
            }
        }

        public string _invDateError;
        public string InvDateError
        {
            get => _invDateError;
            set
            {
                _invDateError = value;
                OnPropertyChange(nameof(InvDateError));
            }
        }

        private string _productControlError;

        public string ProductControlError
        {
            get => _productControlError;
            set
            {
                _productControlError = value;
                OnPropertyChange(nameof(ProductControlError));
            }
        }

        private void AddPost()
        {
            ProductNameError = string.Empty;
            ImagePathError = string.Empty;
            DescriptionError = string.Empty;
            ListPriceError = string.Empty;
            StartPriceError = string.Empty;
            InvDateError = string.Empty;
            ProductControlError = string.Empty;

            ProductNameError = Helpers.validation.IsValidProducttName(ProductName);
            ImagePathError=Helpers.validation.AreValidImagePaths(ImagePaths);
            DescriptionError=Helpers.validation.IsValidDescription(Description);
            StartPriceError = Helpers.validation.IsValidStartPrice(StartPrice);
            ListPriceError=Helpers.validation.IsValidListPrice(ListPrice);
            InvDateError=Helpers.validation.IsValidInvDate(InvDate);

            
            switch (SelectedProductControl)
            {
                case VM_PaintingControl paintingVM when paintingVM.IsValid:
                    {
                        string artist = paintingVM.Artist;
                        int year = paintingVM.Year;
                        decimal length = paintingVM.Length;
                        decimal width = paintingVM.Width;
                        string technique = paintingVM.Technique2;

                        if (string.IsNullOrEmpty(artist) || year <= 0 ||length<=0||width<=0||technique==null)
                        {
                            ProductControlError = "Detaliile produsului sunt incorecte.";
                        }

                        if (Helpers.validation.IsValidPost(ProductNameError, ImagePathError, DescriptionError, ListPriceError, StartPriceError, InvDateError, ProductControlError))
                        {
                            DatabaseManager.AddPaintingPost
                            (_auctionNumber, StartPrice, ListPrice, DateTime.Now, ImagePaths, ProductName,
                            Description, InvDate, technique, artist, year, length, width);

                            ClosePage();
                        }
                        break;
                    }

                case VM_WatchControl watchVM when watchVM.IsValid:
                    {
                        string brand = watchVM.Brand;
                        string type = watchVM.Type;
                        decimal diameter = watchVM.Diameter;
                        string mechanism = watchVM.Mechanism;

                        if (string.IsNullOrEmpty(brand) || type==null || diameter <=0 || mechanism == null)
                        {
                            ProductControlError = "Detaliile produsului sunt incorecte.";
                        }

                        if (Helpers.validation.IsValidPost(ProductNameError, ImagePathError, DescriptionError, ListPriceError, StartPriceError, InvDateError, ProductControlError))
                        {
                            DatabaseManager.AddWatchPost
                            (_auctionNumber, StartPrice, ListPrice, DateTime.Now, ImagePaths, ProductName,
                            Description, InvDate, mechanism, type, diameter, brand);

                            ClosePage();
                        }
                        break;
                    }

                case VM_JewelryControl jewelryVM when jewelryVM.IsValid:
                    {
                        string brand = jewelryVM.Brand;
                        string type = jewelryVM.Type2;
                        //string material = jewelryVM.Material;
                        decimal weight = jewelryVM.Weight;
                        int year = jewelryVM.Year;

                        if (string.IsNullOrEmpty(brand) || weight <=0  || type == null)
                        {
                            ProductControlError = "Detaliile produsului sunt incorecte.";
                        }

                        if (Helpers.validation.IsValidPost(ProductNameError, ImagePathError, DescriptionError, ListPriceError, StartPriceError, InvDateError, ProductControlError))
                        {
                            DatabaseManager.AddJewelryPost(_auctionNumber, StartPrice, ListPrice, DateTime.Now, ImagePaths, ProductName
                            , Description, InvDate, type, brand, weight, year);

                            ClosePage();
                        }
                        break;
                    }
                case VM_BookControl bookVM when bookVM.IsValid:
                    {
                        string author=bookVM.Author;
                        string bookCondition=bookVM.BookCondition2;
                        string language=bookVM.Language;
                        int year=bookVM.Year;
                        int numberOfPage=bookVM.NumberOfPage;
                        string publishingHouse = bookVM.PublishingHouse;

                        if (string.IsNullOrEmpty(author) || bookCondition == null || year <= 0 || string.IsNullOrEmpty(language)||numberOfPage<=0|| string.IsNullOrEmpty(publishingHouse))
                        {
                            ProductControlError = "Detaliile produsului sunt incorecte.";
                        }

                        if (Helpers.validation.IsValidPost(ProductNameError, ImagePathError, DescriptionError, ListPriceError, StartPriceError, InvDateError, ProductControlError))
                        {
                            DatabaseManager.AddBookPost(_auctionNumber, StartPrice, ListPrice, DateTime.Now, ImagePaths, ProductName
                           , Description, InvDate, author, bookCondition, year, publishingHouse, numberOfPage, language);

                            ClosePage();
                        }
                        break;
                    }
                case VM_SculptureControl sculptureVM when sculptureVM.IsValid:
                    {
                        string artist=sculptureVM.Artist;
                        string material=sculptureVM.Material2;
                        decimal width=sculptureVM.Height;
                        decimal length=sculptureVM.Length;
                        decimal depth=sculptureVM.Depth;

                        if (string.IsNullOrEmpty(artist) || length <= 0 || width <= 0 || string.IsNullOrEmpty(material) || depth <= 0 )
                        {
                            ProductControlError = "Detaliile produsului sunt incorecte.";
                        }

                        if (Helpers.validation.IsValidPost(ProductNameError, ImagePathError, DescriptionError, ListPriceError, StartPriceError, InvDateError, ProductControlError))
                        {
                            DatabaseManager.AddSculpturePost(_auctionNumber, StartPrice, ListPrice, DateTime.Now, ImagePaths, ProductName
                           , Description, InvDate, artist, material, width, length, depth);

                            ClosePage();
                        }
                        break;
                    }
         
            }


        }
        private void ClosePage()
        {
            var a = DatabaseManager.GetAuctionByNumber(_auctionNumber);

            var mainWindow = App.Current.Windows
                     .OfType<MainWindow>()
                     .FirstOrDefault();
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new AuctionPage(a));
            }
        }



        private void UpdateSelectedControl()
        {
            SelectedProductControl = ProductType switch
            {
                "Tablouri" => new VM_PaintingControl(),
                "Ceasuri" => new VM_WatchControl(),
                "Bijuterii" => new VM_JewelryControl(),
                "Carti" => new VM_BookControl(),
                "Sculpturi" => new VM_SculptureControl(),
                _ => null,
            };
        }

        public VM_AddPost() { }
    }
}
