using Client_ADBD.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Client_ADBD.Models;
using System.Runtime.InteropServices.ComTypes;
using System.Collections.ObjectModel;

namespace Client_ADBD.ViewModels
{
    internal class VM_ModifyPostPage:VM_Base
    {
        private Post_ _initialPost;

        private int _postId;

        private object _selectedProductControl;
        private int _auctionNumber { get; set; }
        private string _productName { get; set; }
        private decimal _startPrice { get; set; }
        private decimal _listPrice { get; set; }

        private ObservableCollection<string> _imagePaths = new ObservableCollection<string> { null, null, null };
        private string _description { get; set; }
        private DateTime _invDate { get; set; } = DateTime.Now;

        bool isModified=false;

        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                ProductNameError = Helpers.validation.IsValidProducttName(value);
                OnPropertyChange(nameof(ProductName));
            }
        }

        public decimal StartPrice
        {
            get => _startPrice;
            set
            {
                _startPrice = value;
                StartPriceError = Helpers.validation.IsValidStartPrice(value);
                OnPropertyChange(nameof(StartPrice));
            }
        }

        public decimal ListPrice
        {
            get => _listPrice;
            set
            {
                _listPrice = value;
                ListPriceError = Helpers.validation.IsValidListPrice(value);
                OnPropertyChange(nameof(ListPrice));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                DescriptionError = Helpers.validation.IsValidDescription(value);
                OnPropertyChange(nameof(Description));
            }
        }

        public ObservableCollection<string> ImagePaths
        {
            get => _imagePaths;
            set
            {
                if (_imagePaths != value)
                {
                    _imagePaths = value;
                    OnPropertyChange(nameof(ImagePaths));
                }
            }
        }

        public DateTime InvDate
        {
            get => _invDate;
            set
            {
                _invDate = value;
                InvDateError = Helpers.validation.IsValidInvDate(value);
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

        public ICommand SaveChangesCommand { get; set; }

        private bool Admin;
        public VM_ModifyPostPage(Post_ p, bool isAdmin = false)
        {
            Admin = isAdmin;
            _initialPost = p;
            ProductType = p.auctionType;
            _auctionNumber = p.auctionNumber;

            UpdateSelectedControl(p.product);

            _postId = p.postId;
            _auctionNumber = p.auctionNumber;
            _productName = p.product.Name;
            _startPrice = p.product.startPrice;
            _listPrice = p.product.listPrice;
            //_imagePaths = p.product.imagePaths;
            _imagePaths = new ObservableCollection<string>(p.product.imagePaths);
            _description = p.product.Description;
            _invDate=p.product.InventoryDate;

            if (isAdmin == false)
            {
                ClosePageCommand = new RelayCommand(ClosePage);
            }
            else
            {
                ClosePageCommand = new RelayCommand(ClosePageAdmin);
            }

            SaveChangesCommand = new RelayCommand(SaveChanges);
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

        private void SaveChanges()
        {
            ProductNameError = string.Empty;
            ImagePathError = string.Empty;
            DescriptionError = string.Empty;
            ListPriceError = string.Empty;
            StartPriceError = string.Empty;
            InvDateError = string.Empty;
            ProductControlError = string.Empty;

            string[] imagePathArray = ImagePaths.ToArray();

            ProductNameError = Helpers.validation.IsValidProducttName(ProductName);
            ImagePathError = Helpers.validation.AreValidImagePaths(imagePathArray);
            DescriptionError = Helpers.validation.IsValidDescription(Description);
            StartPriceError = Helpers.validation.IsValidStartPrice(StartPrice);
            ListPriceError = Helpers.validation.IsValidListPrice(ListPrice);
            InvDateError = Helpers.validation.IsValidInvDate(InvDate);

            if (ProductName != _initialPost.product.Name ||
                imagePathArray != _initialPost.product.imagePaths ||
                 Description != _initialPost.product.Description ||
                 ListPrice != _initialPost.product.listPrice ||
                 StartPrice != _initialPost.product.startPrice ||
                 InvDate != _initialPost.product.InventoryDate)
            {

                isModified = true;
            }


            switch (SelectedProductControl)
            {
                case VM_PaintingControl paintingVM when paintingVM.IsValid:
                    {
                        string artist = paintingVM.Artist;
                        int year = paintingVM.Year;
                        decimal length = paintingVM.Length;
                        decimal width = paintingVM.Width;
                        string technique = paintingVM.Technique2;

                        if (string.IsNullOrEmpty(artist) || year <= 0 || length <= 0 || width <= 0 || technique == null)
                        {
                            ProductControlError = "Detaliile produsului sunt incorecte.";
                        }

                        if (Helpers.validation.IsValidPost(ProductNameError, ImagePathError, DescriptionError, ListPriceError, StartPriceError, InvDateError, ProductControlError))
                        {
                            if(isModified)
                            {
                                DatabaseManager.UpdateCommonPostDetails(_postId, StartPrice, ListPrice, _initialPost.product.ProductID, ProductName, Description, InvDate, imagePathArray);
                                isModified = false;
                            }
                            if (_initialPost.product is Painting_ painting)
                            {

                                if (artist != painting.Artist ||
                                    year != painting.CreationYear ||
                                    length!= painting.Length ||
                                    width != painting.Width ||
                                    technique!=painting.Type
                                    )
                                {

                                    isModified = true;
                                }
                            }

                            if(isModified)
                            {

                               // DatabaseManager.UpdateCommonPostDetails(_postId, StartPrice, ListPrice, _initialPost.product.ProductID, ProductName, Description, InvDate);
                                DatabaseManager.UpdatePaintingPostDetails(_initialPost.product.ProductID, artist, length, width, technique, year);
                            }

                            if (Admin == true)
                            {
                                ClosePageAdmin();
                            }
                            else
                            {
                                ClosePage();
                            }
                        }
                        break;
                    }

                case VM_WatchControl watchVM when watchVM.IsValid:
                    {
                        string brand = watchVM.Brand;
                        string type = watchVM.Type;
                        decimal diameter = watchVM.Diameter;
                        string mechanism = watchVM.Mechanism;

                        if (string.IsNullOrEmpty(brand) || type == null || diameter <= 0 || mechanism == null)
                        {
                            ProductControlError = "Detaliile produsului sunt incorecte.";
                        }

                        if (Helpers.validation.IsValidPost(ProductNameError, ImagePathError, DescriptionError, ListPriceError, StartPriceError, InvDateError, ProductControlError))
                        {
                            if (isModified)
                            {
                                DatabaseManager.UpdateCommonPostDetails(_postId, StartPrice, ListPrice, _initialPost.product.ProductID, ProductName, Description, InvDate, imagePathArray);
                                isModified = false;
                            }
                            if (_initialPost.product is Watch_ watch)
                            {


                                if (brand != watch.Manufacturer ||
                                    type != watch.Type||
                                    diameter!=watch.Diameter ||
                                    mechanism!=watch.Mechanism 
                                    )
                                {

                                    isModified = true;
                                }
                            }

                            if (isModified)
                            {
                                DatabaseManager.UpdateWatchPostDetails(_initialPost.product.ProductID, diameter, brand, type, mechanism);
                            }
                            if (Admin == true)
                            {
                                ClosePageAdmin();
                            }
                            else
                            {
                                ClosePage();
                            }
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

                        if (string.IsNullOrEmpty(brand) || weight <= 0 || type == null)
                        {
                            ProductControlError = "Detaliile produsului sunt incorecte.";
                        }

                        if (Helpers.validation.IsValidPost(ProductNameError, ImagePathError, DescriptionError, ListPriceError, StartPriceError, InvDateError, ProductControlError))
                        {
                            if(isModified)
                            {
                                isModified = false;
                                DatabaseManager.UpdateCommonPostDetails(_postId, StartPrice, ListPrice, _initialPost.product.ProductID, ProductName, Description, InvDate, imagePathArray);
                            }
                            if (_initialPost.product is Jewelry_ jwl)
                            {

                                if (brand != jwl.Brand||
                                    year != jwl.CreationYear ||
                                    weight != jwl.Weight||
                                    type != jwl.Type)
                                {

                                    isModified = true;
                                }
                            }

                            if (isModified)
                            {
                                DatabaseManager.UpdateJewelryPostDetails(_initialPost.product.ProductID, brand, weight, year, type);
                            }
                            if (Admin == true)
                            {
                                ClosePageAdmin();
                            }
                            else
                            {
                                ClosePage();
                            }
                        }
                        break;
                    }
                case VM_BookControl bookVM when bookVM.IsValid:
                    {
                        string author = bookVM.Author;
                        string bookCondition = bookVM.BookCondition2;
                        string language = bookVM.Language;
                        int year = bookVM.Year;
                        int numberOfPage = bookVM.NumberOfPage;
                        string publishingHouse = bookVM.PublishingHouse;

                        if (string.IsNullOrEmpty(author) || bookCondition == null || year <= 0 || string.IsNullOrEmpty(language) || numberOfPage <= 0 || string.IsNullOrEmpty(publishingHouse))
                        {
                            ProductControlError = "Detaliile produsului sunt incorecte.";
                        }

                        if (Helpers.validation.IsValidPost(ProductNameError, ImagePathError, DescriptionError, ListPriceError, StartPriceError, InvDateError, ProductControlError))
                        {
                            if(isModified)
                            {
                                DatabaseManager.UpdateCommonPostDetails(_postId, StartPrice, ListPrice, _initialPost.product.ProductID, ProductName, Description, InvDate, imagePathArray);
                                isModified = false;
                            }

                            if (_initialPost.product is Book_ book)
                            {

                                if (author != book.Author ||
                                    year != book.PublicationYear ||
                                    bookCondition!=book.Condition ||
                                    language!=book.Language ||
                                    numberOfPage!=book.PageNumber||
                                     publishingHouse!=book.PublishingHouse)
                                {

                                    isModified = true;
                                }
                            }

                            if (isModified)
                            {
                                DatabaseManager.UpdateBookPostDetails(_initialPost.product.ProductID, author, year, publishingHouse, numberOfPage, language, bookCondition);
                            }

                            if (Admin == true)
                            {
                                ClosePageAdmin();
                            }
                            else
                            {
                                ClosePage();
                            }
                        }
                        break;
                    }
                case VM_SculptureControl sculptureVM when sculptureVM.IsValid:
                    {
                        string artist = sculptureVM.Artist;
                        string material = sculptureVM.Material2;
                        decimal width = sculptureVM.Height;
                        decimal length = sculptureVM.Length;
                        decimal depth = sculptureVM.Depth;

                        if (string.IsNullOrEmpty(artist) || length <= 0 || width <= 0 || string.IsNullOrEmpty(material) || depth <= 0)
                        {
                            ProductControlError = "Detaliile produsului sunt incorecte.";
                        }

                        if (Helpers.validation.IsValidPost(ProductNameError, ImagePathError, DescriptionError, ListPriceError, StartPriceError, InvDateError, ProductControlError))
                        {
                            if(isModified)
                            {
                                DatabaseManager.UpdateCommonPostDetails(_postId, StartPrice, ListPrice, _initialPost.product.ProductID, ProductName, Description, InvDate, imagePathArray);
                                isModified = false;
                            }

                            if (_initialPost.product is Sculpture_ scl)
                            {

                                if (artist != scl.Artist ||
                                    material != scl.Material ||
                                    length != scl.Length ||
                                    width != scl.Width ||
                                    depth != scl.Depth )
                                {

                                    isModified = true;
                                }
                            }

                            if (isModified)
                            {
                                DatabaseManager.UpdateSculpturePostDetails(_initialPost.product.ProductID, artist, length, width, depth, material);
                            }

                            if (Admin == true)
                            {
                                ClosePageAdmin();
                            }
                            else
                            {
                                ClosePage();
                            }
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

        private void ClosePageAdmin()
        {
            var a = DatabaseManager.GetAuctionByNumber(_auctionNumber);

            var adminWindow = App.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
            var frame = adminWindow?.FindName("AdminFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new AuctionPage(a, false, false, false, true));
            }
        }

        private void UpdateSelectedControl(IProduct pr)
        {
            SelectedProductControl = ProductType switch
            {
                "Tablouri" when pr is Painting_ painting => new VM_PaintingControl(painting),
                "Ceasuri" when pr is Watch_ watch => new VM_WatchControl(watch),
                "Bijuterii" when pr is Jewelry_ jewelry => new VM_JewelryControl(jewelry),
                "Carti" when pr is Book_ book => new VM_BookControl(book),
                "Sculpturi" when pr is Sculpture_ sculpture => new VM_SculptureControl(sculpture),
                _ => null,
            };
        }

        public VM_ModifyPostPage() { }
    }
}
