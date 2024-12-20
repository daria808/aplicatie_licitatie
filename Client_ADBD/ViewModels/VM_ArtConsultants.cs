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
    internal class VM_ArtConsultants : VM_Base
    {
        public ICommand BackCommand { get; set; }
        
        public VM_ArtConsultants()
        {
            // Assign back navigation logic
            BackCommand = new RelayCommand(OnBack);
        }

        private void OnBack()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_ContactPage());
            }
        }

    }
}
