using Client_ADBD.Models;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Client_ADBD.ViewModels
{
    internal class VM_MainWindow : VM_Base
    {

        private object _selectedViewModel;

        public ICommand ShowMainPageCommand { get; }
        public ICommand ShowAccountCommand { get; }
        public ICommand ShowAboutCommand { get; }
        public ICommand ShowStatisticsCommand { get; }

        public VM_MainWindow()
        {
           
            ShowMainPage();
            ShowMainPageCommand=new RelayCommand(ShowMainPage);
            ShowAccountCommand = new RelayCommand(ShowAccount);

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

        private void ShowAccount()
        {
            SelectedViewModel = new VM_Account();
        }

        private void ShowMainPage()
        {
            SelectedViewModel=new VM_MainPage();
        }

    }
}
