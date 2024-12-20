using Client_ADBD.Helpers;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Client_ADBD.ViewModels
{
    internal class VM_AdminWindow : VM_Base
    {

        private object _selectedViewModel;

        public ICommand BackCommand { get; set; }
        public ICommand ShowUserDetailes {  get; set; }
        public ICommand RemoveUser { get; set; }
        public ICommand RemoveAuctions { get; set; }
        public ICommand ShowNotifications { get; set; }
        public VM_AdminWindow()
        {

            ShowMainAdminPage();
            ShowUserDetailes = new RelayCommand(OnShowUserDetailes);
            BackCommand = new RelayCommand(OnBackPressed);
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

        private void OnShowUserDetailes()
        {
            SelectedViewModel = new Vm_UsersDetailes();
        }

        private void ShowMainAdminPage()
        {
            SelectedViewModel = new VM_MainAdminPage();
        }
    }
}
