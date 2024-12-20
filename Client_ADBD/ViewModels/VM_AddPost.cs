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
        private int auctionNumber {  get; set; }
        private string _productName {  get; set; }
        private decimal _startPrice {  get; set; }
        private decimal _listPrice {  get; set; }
        private string _imagePath {  get; set; }
        private string _description {   get; set; }
        private DateTime _invDate { get; set; }=DateTime.Now;

        public string ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                OnPropertyChange(nameof(ProductName));
            }
        }

        public decimal StartPrice
        {
            get => _startPrice;
            set
            {
                _startPrice = value;
                OnPropertyChange(nameof(StartPrice));
            }
        }

        public decimal ListPrice
        {
            get => _listPrice;
            set
            {
                _listPrice = value;
                OnPropertyChange(nameof(ListPrice));
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChange(nameof(Description));
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

        public DateTime InvDate
        {
            get => _invDate;
            set
            {
                _invDate = value;
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
            this.auctionNumber=auctionNumber;
            UpdateSelectedControl();
            ClosePageCommand=new RelayCommand(ClosePage);
            AddPostCommand = new RelayCommand(AddPost);
        }

        private void AddPost()
        {
            switch (SelectedProductControl)
            {
                case VM_PaintingControl paintingVM when paintingVM.IsValid:
                    {
                        string artist = paintingVM.Artist;
                        int year = paintingVM.Year;
                        decimal length = paintingVM.Length;
                        decimal width = paintingVM.Width;
                        string technique = paintingVM.Technique2;

                        DatabaseManager.AddPaintingPost
                            (auctionNumber, StartPrice, ListPrice, DateTime.Now, ImagePath, ProductName,
                            Description, InvDate,technique,artist,year, length,width);

                        ClosePage();
                        break;
                    }

                case VM_WatchControl watchVM when watchVM.IsValid:
                    {
                        string brand = watchVM.Brand;
                        string type = watchVM.Type;
                        int diameter = watchVM.Diameter;
                        string mechanism = watchVM.Mechanism;

                        DatabaseManager.AddWatchPost
                            (auctionNumber, StartPrice, ListPrice, DateTime.Now, ImagePath, ProductName,
                            Description, InvDate, mechanism,type, diameter, brand);

                        ClosePage();
                        break;
                    }

                case VM_JewelryControl jewelryVM when jewelryVM.IsValid:
                    {
                        string brand = jewelryVM.Brand;
                        string type = jewelryVM.Type2;
                        string material = jewelryVM.Material;
                        decimal weight = jewelryVM.Weight;
                        int year = jewelryVM.Year;

                        DatabaseManager.AddJewelryPost(auctionNumber,StartPrice,ListPrice, DateTime.Now, ImagePath,ProductName
                            ,Description,InvDate,type,brand,weight,year);

                        ClosePage();
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

                        DatabaseManager.AddBookPost(auctionNumber, StartPrice, ListPrice, DateTime.Now, ImagePath, ProductName
                           , Description, InvDate,author,bookCondition,year,publishingHouse,numberOfPage,language);

                        ClosePage();
                        break;
                    }
                case VM_SculptureControl sculptureVM when sculptureVM.IsValid:
                    {
                        string artist=sculptureVM.Artist;
                        string material=sculptureVM.Material2;
                        decimal width=sculptureVM.Height;
                        decimal length=sculptureVM.Length;
                        decimal depth=sculptureVM.Depth;

                        DatabaseManager.AddSculpturePost(auctionNumber, StartPrice, ListPrice, DateTime.Now, ImagePath, ProductName
                           , Description, InvDate,artist,material, width, length, depth);

                        ClosePage();
                           
                        break;
                    }
                //default:
                //    {
                  
                //        break;
                //    }
            }


        }
        private void ClosePage()
        {
            var a = DatabaseManager.GetAuctionByNumber(auctionNumber);

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
