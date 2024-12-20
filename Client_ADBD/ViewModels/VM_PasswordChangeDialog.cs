using Client_ADBD.Models;
using Client_ADBD.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client_ADBD.ViewModels
{
    internal class VM_PasswordChangeDialog : VM_Base
    {
        private string _newPassword;
        private string _confirmPassword;

        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChange(nameof(NewPassword));
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChange(nameof(ConfirmPassword));
            }
        }

        public ICommand ChangePasswordCommand { get; set; }


        public User_ currentUser;
        public VM_PasswordChangeDialog(User_ user)
        {
            currentUser = user;

            ChangePasswordCommand = new RelayCommand(OnChangePasswordPressed);
        }

        private void OnChangePasswordPressed()
        {
            if (string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(ConfirmPassword))
            {
                MessageBox.Show("Te rugăm să introduci atât parola nouă cât și confirmarea parolei.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (NewPassword != ConfirmPassword)
            {
                MessageBox.Show("Parolele nu se potrivesc. Vă rugăm să încercați din nou.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (NewPassword.Length < 8 || ConfirmPassword.Length < 8)
            {
                MessageBox.Show("Parola trebuie să aibă cel puțin 8 caractere.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (currentUser == null) return;

            bool successful = (new DatabaseManager()).AdminChangeUserPassword(currentUser._username, NewPassword);

            if (successful)
            {
                MessageBox.Show("Parolă modificată cu succes!");
            }
            else
            {
                MessageBox.Show("Eroare la modificarea parolei utilizatorului!");
            }

        }
    }
}
