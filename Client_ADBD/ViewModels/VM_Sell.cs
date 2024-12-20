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
    internal class VM_Sell
    {
        public ICommand BackCommand { get; }
        public ICommand RegulationsCommand { get; }
        public ICommand AdvanceConsignationCommand { get; }
        public ICommand RegulationsSolicitationCommand { get; }

        public VM_Sell()
        {
            BackCommand = new RelayCommand(OnBack);
            RegulationsCommand = new RelayCommand(OnRegulationsPressed);
            AdvanceConsignationCommand = new RelayCommand(OnAdvanceConsignationPressed);
            RegulationsSolicitationCommand = new RelayCommand(OnRegulationsSPressed);
        }

        private void OnRegulationsSPressed()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_RegsSolicitations());
            }
        }
        private void OnAdvanceConsignationPressed()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_ConsignationAdvance());
            }
        }
        private void OnRegulationsPressed()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_RegsConsignations());
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
    }
}
