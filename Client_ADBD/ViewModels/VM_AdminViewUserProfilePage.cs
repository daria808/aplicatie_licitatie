using Client_ADBD.Models;
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
    internal class VM_AdminViewUserProfilePage : VM_Base
    {
        public ICommand BackCommand { get; set; }

        public VM_AdminViewUserProfilePage(User_ user)
        { 
            BackCommand = new RelayCommand(OnBack);
        }

        private void OnBack()
        {
            var adminWindow = App.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
            var frame = adminWindow?.FindName("AdminFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new Vm_UsersDetailes());
            }
        }
    }
}
