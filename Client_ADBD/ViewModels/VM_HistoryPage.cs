using Client_ADBD.Views;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Client_ADBD.Helpers;

namespace Client_ADBD.ViewModels
{
    internal class VM_HistoryPage : VM_Base
    {
        public ICommand BackCommand { get; }

        public VM_HistoryPage()
        {
            BackCommand = new RelayCommand(OnBack);
            NavigateTo2024Command = new RelayCommand(ExecuteNavigateTo2024);
            NavigateToDecemberCommand = new RelayCommand(ExecuteNavigateToDecember);
            NavigateToNovemberCommand = new RelayCommand(ExecuteNavigateToNovember);
            NavigateToSeptemberCommand = new RelayCommand(ExecuteNavigateToSeptember);
            NavigateToJulyCommand = new RelayCommand(ExecuteNavigateToJuly);
        }
        private void OnBack()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_AboutUs());
            }
        }

        //
        public ICommand NavigateTo2024Command { get; }
        public ICommand NavigateToDecemberCommand { get; }
        public ICommand NavigateToNovemberCommand { get; }
        public ICommand NavigateToSeptemberCommand { get; }
        public ICommand NavigateToJulyCommand { get; }

        private ScrollViewer _historyScrollViewer;


        // Referință la ScrollViewer din View
        public void SetScrollViewer(ScrollViewer scrollViewer)
        {
            _historyScrollViewer = scrollViewer;
        }

        // Implementare pentru fiecare secțiune
        private void ExecuteNavigateTo2024()
        {
            ScrollToSection("Section2024");
        }

        private void ExecuteNavigateToDecember()
        {
            ScrollToSection("SectionDecember");
        }

        private void ExecuteNavigateToNovember()
        {
            ScrollToSection("SectionNovember");
        }

        private void ExecuteNavigateToSeptember()
        {
            ScrollToSection("SectionSeptember");
        }

        private void ExecuteNavigateToJuly()
        {
            ScrollToSection("SectionJuly");
        }

        private void ScrollToSection(string sectionName)
        {
            if (_historyScrollViewer != null)
            {
                var element = _historyScrollViewer.FindName(sectionName) as UIElement;
                if (element != null)
                {
                    _historyScrollViewer.ScrollToElement(element);
                }
            }

        }
    }
}
