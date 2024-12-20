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
    internal class VM_Buy
    {
        public ICommand CertificateCommand { get; set; }
        public ICommand AuctionsCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand ParticipateAuctionsCommand { get; set; }

        public ICommand RegulationsCommand { get; set; }
        public ICommand RatePaymentCommand { get; set; }

        public VM_Buy() {
            CertificateCommand = new RelayCommand(ShowCertificate);
            AuctionsCommand = new RelayCommand(ShowOngoingAuctions);
            BackCommand = new RelayCommand(OnBack);
            ParticipateAuctionsCommand = new RelayCommand(ShowParticipateAuctions);
            RegulationsCommand = new RelayCommand(ShowRegulations);
            RatePaymentCommand = new RelayCommand(ShowRatePayment);
        }


        private void ShowRatePayment()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_RatePayment());
            }
        }
        private void ShowRegulations()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_Regulations());
            }
        }
        private void ShowCertificate()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_GiftCertificate());
            }
        }

        private void ShowOngoingAuctions()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_OngoingAuctions());
            }
        }

        private void OnBack()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_MainPage());
            }
        }

        private void ShowParticipateAuctions()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_ParticipateAuctions());
            }
        }
    }
}
