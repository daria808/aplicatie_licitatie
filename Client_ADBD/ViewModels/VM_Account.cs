using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Client_ADBD.ViewModels;
using Client_ADBD.Models;
using Client_ADBD.Views;
using System.Windows;
using System.IO.Packaging;
using Client_ADBD.Helpers;
using System.Windows.Controls;

namespace Client_ADBD.ViewModels
{
    internal class VM_Account : VM_Base
    {

        private string oldPassword;
        private string newPassword;
        private string confirmPassword;

        public ICommand BackCommand { get; set; }

        public string OldPassword
        {
            get { return oldPassword; }
            set
            {
                if (oldPassword != value)
                {
                    oldPassword = value;
                    OnPropertyChange(nameof(OldPassword));
                }
            }
        }

        public string NewPassword
        {
            get { return newPassword; }
            set
            {
                if (newPassword != value)
                {
                    newPassword = value;
                    OnPropertyChange(nameof(NewPassword));
                }
            }
        }

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                if (confirmPassword != value)
                {
                    confirmPassword = value;
                    OnPropertyChange(nameof(ConfirmPassword));
                }
            }
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; set; }
        public string WelcomeMessage { get; set; }
        public string CongratsMessage { get; set; }
        public string RoleMessage { get; set; }
        public string OptionMessage { get; set; }

        public ICommand SaveCommand { get; }


        // Proprietăți pentru RadioButton
        private bool _isYesSelected;
        private bool _isNoSelected;

        public bool IsYesSelected
        {
            get { return _isYesSelected; }
            set
            {
                if (_isYesSelected != value)
                {
                    _isYesSelected = value;
                    OnPropertyChange(nameof(IsYesSelected));

                    // Logica de schimbare între butoane
                    if (value) IsNoSelected = false;
                    UpdateDatabaseForYesOption();
                }
            }
        }

        public bool IsNoSelected
        {
            get { return _isNoSelected; }
            set
            {
                if (_isNoSelected != value)
                {
                    _isNoSelected = value;
                    OnPropertyChange(nameof(IsNoSelected));

                    // Logica de schimbare între butoane
                    if (value) IsYesSelected = false;
                }
            }
        }

        private VM_MainWindow _mainWindow; // Proprietate pentru stocarea mainWindow
        private User_ user;
        public ICommand ChangePasswordCommand { get; }

        public VM_Account(User_ currentUser, VM_MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            user = currentUser;
            ChangePasswordCommand = new RelayCommand(ChangePassword);


            Username = currentUser._username;
            Email = currentUser._email;
            FirstName = currentUser._firstName;
            LastName = currentUser._lastName;
            Balance = currentUser._balance ?? 0.00M;

            if (currentUser != null)
            {
                try
                {
                    int roleId = (new DatabaseManager()).GetRoleIdByUsername(currentUser._username);

                    switch (roleId)
                    {
                        case 2: // Bidder
                            WelcomeMessage = $"Bun venit la AuctionPro, {FirstName}!";
                            ShowBidderInterface();
                            break;
                        case 3: // Client
                            WelcomeMessage = $"Bun venit la AuctionPro, {FirstName}!";
                            ShowClientInterface();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Eroare la obținerea RoleId: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Eroare: Utilizatorul nu a fost găsit!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            SaveCommand = new RelayCommand(() => SaveChanges());
            BackCommand = new RelayCommand(GoBack);
        }

        private void GoBack()
        {
            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_MainPage());
            }
        }
        private void SaveChanges()
        {
            try
            {
                // Logica de salvare: actualizează baza de date sau altă sursă de date
                var dbManager = new DatabaseManager();

                dbManager.UpdateUser(Username, FirstName, LastName, Email, Balance);

                var user = (new DatabaseManager()).GetUsers().FirstOrDefault(u => u._username == Username);
                if (user != null)
                {
                    CurrentUser.User = user;  // Setează utilizatorul curent cu cel actualizat


                    _mainWindow.SelectedViewModel = new VM_Account(CurrentUser.User, _mainWindow);

                }


                MessageBox.Show("Modificările au fost salvate cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la salvarea modificărilor: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void ChangePassword()
        {
            try
            {
                // Verifică dacă toate câmpurile sunt completate
                if (string.IsNullOrWhiteSpace(OldPassword) ||
                    string.IsNullOrWhiteSpace(NewPassword) ||
                    string.IsNullOrWhiteSpace(ConfirmPassword))
                {
                    MessageBox.Show("Toate câmpurile trebuie completate!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Verifică dacă noua parolă și confirmarea parolei sunt identice
                if (NewPassword != ConfirmPassword)
                {
                    MessageBox.Show("Noua parolă și confirmarea parolei nu coincid.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Verifică dacă noua parolă are cel puțin 8 caractere
                if (NewPassword.Length < 8)
                {
                    MessageBox.Show("Noua parolă trebuie să aibă cel puțin 8 caractere.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var dbManager = new DatabaseManager();
                bool isPasswordChanged = dbManager.ChangeUserPassword(Username, OldPassword, NewPassword);

                if (isPasswordChanged)
                {
                    MessageBox.Show("Parola a fost schimbată cu succes!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Golește câmpurile de parolă după schimbarea parolei
                    OldPassword = string.Empty;
                    NewPassword = string.Empty;
                    ConfirmPassword = string.Empty;

                    // Dacă folosești PasswordBox-uri, trebuie să golim manual și acestea

                    ResetPasswordFields();
                }
                else
                {
                    MessageBox.Show("Parola veche este incorectă!", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la schimbarea parolei: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetPasswordFields()
        {
            OldPassword = string.Empty;
            NewPassword = string.Empty;
            ConfirmPassword = string.Empty;

            var mainWindow = App.Current.MainWindow as MainWindow;
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_Account(user, _mainWindow));
            }


        }


        public void ShowClientInterface()
        {
            RoleMessage = $"În aplicația noastră de licitații, ai acces la o selecție exclusivă de obiecte de artă pe care le poți achiziționa!";
            OptionMessage = $"Vrei să devii ofertant?";
        }

        public void ShowBidderInterface()
        {

            RoleMessage = $"În cadrul aplicației noastre de licitații, ai posibilitatea de a cumpăra obiecte unice și de a " +
               $"adăuga propriile tale articole pentru a le scoate la licitație!";
            CongratsMessage = "Ai acces la toate facilitățile disponibile.";
        }



        private void UpdateDatabaseForYesOption()
        {
            try
            {
                var dbManager = new DatabaseManager();
                dbManager.UpdateUserRoleToBidder(Username);

                MessageBox.Show("Felicitări! Rolul tău a fost actualizat la ofertant.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);

                var user = (new DatabaseManager()).GetUsers().FirstOrDefault(u => u._username == Username);
                if (user != null)
                {
                    CurrentUser.User = user;  // Setează utilizatorul curent cu cel actualizat


                    _mainWindow.SelectedViewModel = new VM_Account(CurrentUser.User, _mainWindow);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la actualizarea rolului: {ex.Message}", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}