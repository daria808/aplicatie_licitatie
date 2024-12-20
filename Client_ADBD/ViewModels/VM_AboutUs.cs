using Client_ADBD.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client_ADBD.ViewModels
{
    internal class VM_AboutUs : VM_Base
    {
        public ICommand NavigateToAboutCommand { get; }
        public ICommand NavigateToHistoryCommand { get; }
        public ICommand NavigateToExpertsCommand { get; }
        public ICommand NavigateToContactCommand { get; }

        public ICommand BackCommand { get; }

        public ICommand HandleSelectionCommand { get; }


        private string _selectedItem;
        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChange(nameof(SelectedItem)); // Notifică UI-ul despre schimbarea valorii
                    HandleSelectionCommand.Execute(_selectedItem);
                }
            }
        }


        public VM_AboutUs()
        {
            HandleSelectionCommand = new RelayCommand<string>(HandleListBoxSelection);

            // Assign back navigation logic
            BackCommand = new RelayCommand(OnBack);

            // Commands for image navigation
            NavigateToAboutCommand = new RelayCommand(NavigateToAbout);
            NavigateToHistoryCommand = new RelayCommand(NavigateToHistory);
            NavigateToExpertsCommand = new RelayCommand(NavigateToExperts);
            NavigateToContactCommand = new RelayCommand(NavigateToContact);
        }

        private void HandleListBoxSelection(string selectedItem)
        {
            switch (selectedItem)
            {
                case "Despre noi":
                    NavigateToAbout();
                    break;
                case "Istoric AuctionPro":
                    NavigateToHistory();
                    break;
                case "Experți":
                    NavigateToExperts();
                    break;
                case "Contact":
                    NavigateToContact();
                    break;
            }
        }

        public void NavigateToContact()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                // Navighează către AddAuctionPage
                frame.Navigate(new VM_ContactPage());
            }
        }

        public void NavigateToAbout()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                // Navighează către AddAuctionPage
                frame.Navigate(new VM_About());
            }
        }

        public void NavigateToHistory()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                // Navighează către AddAuctionPage
                frame.Navigate(new VM_HistoryPage());
            }
        }

        public void NavigateToExperts()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                // Navighează către AddAuctionPage
                frame.Navigate(new VM_ExpertsPage());
            }
        }

        private void OnBack()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                // Navighează către AddAuctionPage
                frame.Navigate(new VM_MainPage());
            }
        }
    }
}
