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
    internal class VM_RatePayment
    {
        public ICommand BackCommand { get; }
        public ICommand BuyCommand { get; }
        public ICommand ConsultantsCommand { get; }

        public VM_RatePayment()
        {
            BackCommand = new RelayCommand(OnBack);
            ConsultantsCommand = new RelayCommand(OnConsultantsPressed);
            BuyCommand = new RelayCommand(OnBuyPressed);
        }

        private void OnConsultantsPressed()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_ArtConsultants());
            }
        }

        private void OnBuyPressed()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_Buy());
            }
        }

        private void OnBack()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_Buy());
            }
        }
    }
}
