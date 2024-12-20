using Client_ADBD.Views;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client_ADBD.ViewModels
{
    internal class VM_ContactPage : VM_Base
    {
        private object _selectedViewModel;
        
        public ICommand BackCommand { get; }
        public ICommand ConsultantsCommand { get; set; }

        public object SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                OnPropertyChange(nameof(SelectedViewModel));
            }
        }

        public VM_ContactPage()
        {
            // Assign back navigation logic
            BackCommand = new RelayCommand(OnBack);
            ConsultantsCommand = new RelayCommand(OnConsultants);
        }

        private void OnBack()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                // Navighează către AddAuctionPage
                frame.Navigate(new VM_AboutUs());
            }
        }

        private void OnConsultants()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_ArtConsultants());
            }
        }
    }
}
