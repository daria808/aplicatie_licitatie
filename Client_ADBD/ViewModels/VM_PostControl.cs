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

namespace Client_ADBD.ViewModels
{//Current Price: $0"
    internal class VM_PostControl:VM_Base
    {
        private int _id;
        private string _imagePath;
        private string _name;
        private string _author;
        private decimal _startPrice;
        private string _status;

        public ICommand GoToPostDetailsPageCommand {  get; set; }

        public VM_PostControl() {
            GoToPostDetailsPageCommand = new RelayCommand(GotoPostDetailsPage);
        }

        private void GotoPostDetailsPage()
        {
            //var p = DatabaseManager.GetPostStatusById(Id);
            Post_ p = DatabaseManager.GetPostDetails(_id);


            var mainWindow = App.Current.Windows
                     .OfType<MainWindow>()
                     .FirstOrDefault();
            var frame = mainWindow?.FindName("MainFrame") as Frame;


            if (frame != null)
            {
                frame.Navigate(new PostPage(p));
            }
        }
        public int Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChange(nameof(Id));
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

        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChange(nameof(Auction));
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
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChange(nameof(Status));
            }
        }

    }
}
