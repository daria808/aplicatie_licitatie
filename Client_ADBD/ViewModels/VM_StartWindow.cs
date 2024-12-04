using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Client_ADBD.Helpers;

namespace Client_ADBD.ViewModels
{
    internal class VM_StartWindow:VM_Base
    {
        public ICommand NavigateCommand { get; }

        public VM_StartWindow() 
        {
            NavigateCommand = new RelayCommand(OnNavigate);
        }

        private void OnNavigate()
        {
            NavigationService.NavigateTo("LogInWindow");
        }
    }

}
