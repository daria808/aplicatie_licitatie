using Client_ADBD.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client_ADBD.ViewModels
{
    internal class VM_GiftCertificate : VM_Base
    {
        public ICommand BackCommand { get; }
        public ICommand SendCommand { get; }

        public VM_GiftCertificate()
        {
            BackCommand = new RelayCommand(OnBack);
            SendCommand = new RelayCommand(OnSend);

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


        private string _nume;
        private string _prenume;
        private string _email;
        private string _telefon;
        private string _doamneiDomnului;
        private string _dinPartea;
        private string _valoare;
        private string _cuOcazia;
        private bool _isAgreementChecked;

        public string Nume
        {
            get => _nume;
            set { _nume = value; OnPropertyChange(nameof(Nume)); }
        }

        public string Prenume
        {
            get => _prenume;
            set { _prenume = value; OnPropertyChange(nameof(Prenume)); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChange(nameof(Email)); }
        }

        public string Telefon
        {
            get => _telefon;
            set { _telefon = value; OnPropertyChange(nameof(Telefon)); }
        }

        public string DoamneiDomnului
        {
            get => _doamneiDomnului;
            set { _doamneiDomnului = value; OnPropertyChange(nameof(DoamneiDomnului)); }
        }

        public string DinPartea
        {
            get => _dinPartea;
            set { _dinPartea = value; OnPropertyChange(nameof(DinPartea)); }
        }

        public string Valoare
        {
            get => _valoare;
            set { _valoare = value; OnPropertyChange(nameof(Valoare)); }
        }

        public string CuOcazia
        {
            get => _cuOcazia;
            set { _cuOcazia = value; OnPropertyChange(nameof(CuOcazia)); }
        }

        public bool IsAgreementChecked
        {
            get => _isAgreementChecked;
            set { _isAgreementChecked = value; OnPropertyChange(nameof(IsAgreementChecked)); }
        }


        private void OnSend()
        {

            // Validare câmpuri
            string errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(Nume))
                errorMessage += "Numele este obligatoriu.\n";

            if (string.IsNullOrWhiteSpace(Prenume))
                errorMessage += "Prenumele este obligatoriu.\n";

            if (string.IsNullOrWhiteSpace(Email) || !Regex.IsMatch(Email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                errorMessage += "Adresa de email nu este validă.\n";

            if (string.IsNullOrWhiteSpace(Telefon) || !Regex.IsMatch(Telefon, @"^\+?[0-9]{10,13}$"))
                errorMessage += "Numărul de telefon nu este valid.\n";

            if (string.IsNullOrWhiteSpace(DoamneiDomnului))
                errorMessage += "Câmpul 'Doamnei/Domnului' este obligatoriu.\n";

            if (string.IsNullOrWhiteSpace(DinPartea))
                errorMessage += "Câmpul 'Cu drag, din partea' este obligatoriu.\n";

            if (string.IsNullOrWhiteSpace(Valoare))
                errorMessage += "Câmpul 'Opere de artă în valoare de' este obligatoriu.\n";

            if (string.IsNullOrWhiteSpace(CuOcazia))
                errorMessage += "Câmpul 'Cu ocazia' este obligatoriu.\n";

            if (!IsAgreementChecked)
                errorMessage += "Trebuie să bifați că sunteți de acord cu prelucrarea datelor personale.\n";

            // Afișare erori
            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage, "Erori în formular", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Procesare formular (exemplu: trimite datele mai departe)
            MessageBox.Show("Formular trimis cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            OnBack();
        }
    }
}
