using Client_ADBD.Helpers;
using Client_ADBD.Models;
using Client_ADBD.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace Client_ADBD.ViewModels
{
    internal class VM_MainWindow : VM_Base
    {

        private object _selectedViewModel;

        private string _status;

        public ICommand BackCommand { get; set; }
        public ICommand ShowMainPageCommand { get; }
        public ICommand ShowAccountCommand { get; }
        public ICommand ShowAboutCommand { get; }
        public ICommand ShowStatisticsCommand { get; }
        public ICommand ShowHowToBuy { get; }
        public ICommand ShowHowToSell { get; }
        public ICommand ExitCommand { get; }
        public VM_MainWindow()
        {

            ShowMainPage();
            BackCommand = new RelayCommand(OnBackPressed);
            ShowStatisticsCommand = new RelayCommand(OnShowStatisticsPressed);
            ShowAccountCommand = new RelayCommand(ShowAccount);
            ShowAboutCommand = new RelayCommand(ShowAbout);
            ShowHowToBuy = new RelayCommand(ShowBuy);
            ShowHowToSell = new RelayCommand(ShowSell);
            ExitCommand = new RelayCommand(Exit);
        }

        private void Exit()
        {
            var result = MessageBox.Show(
                "Doriți să părăsiți aplicația?",
                "Exit",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                Utilities.Status=value;
                OnPropertyChange(nameof(Status));
            }
        }
        public object SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                _selectedViewModel = value;
                OnPropertyChange(nameof(SelectedViewModel));
            }
        }

        public void OnBackPressed()
        {
            NavigationService.NavigateTo("LogInWindow");
        }

        public void OnShowStatisticsPressed()
        {
            SelectedViewModel = new VM_Statistics();
        }
        public void ShowAccount()
        {
            var currentUser = CurrentUser.User;  // Accesăm utilizatorul curent
            if (currentUser != null)
            {
                SelectedViewModel = new VM_Account(currentUser, this);  // Transmit utilizatorul curent
            }
            else
            {
                // Dacă currentUser este null, afișează un MessageBox
                MessageBox.Show("Eroare: Utilizatorul nu a fost găsit in mainwindow!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ShowSell()
        {
            SelectedViewModel = new VM_Sell();
        }

        public void ShowAbout()
        {
            SelectedViewModel = new VM_AboutUs();
        }

        public void ShowBuy()
        {
            SelectedViewModel = new VM_Buy();
        }

        private void ShowMainPage()
        {
            SelectedViewModel = new VM_MainPage();
        }

    }
}