using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Client_ADBD.Helpers;
using System.Windows;

namespace Client_ADBD.ViewModels
{
    
    internal class VM_ErrorWindow:VM_Base
    {
        public string ErrorMessage { get; set; }
        public ICommand CloseCommand { get; }

        public VM_ErrorWindow(string errorMessage)
        {
            CloseCommand = new RelayCommand(CloseWindow);
            this.ErrorMessage = errorMessage;
        }

        public VM_ErrorWindow() { }



    }
}
