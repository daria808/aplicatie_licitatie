using Client_ADBD.Models;
using Client_ADBD.Views;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client_ADBD.ViewModels
{
    internal class Vm_UsersDetailes : VM_Base
    {
 
        public int DataGridHeight { get; set; }

        public ICommand DeleteUser { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand ChangeRole { get; set; }
        public ICommand ViewProfile { get; set; }

        private ICommand _openPasswordChangeDialogCommand;

        public ICommand OpenPasswordChangeDialogCommand
        {
            get
            {
                return _openPasswordChangeDialogCommand ??= new RelayCommand<User_>(OpenPasswordChangeDialog);
            }
        }

        public ICommand ChangePasswordCommand { get; }

        private void OpenPasswordChangeDialog(User_ user)
        {
            var passwordChangeDialog = new PasswordChangeDialog();
            passwordChangeDialog.DataContext = new VM_PasswordChangeDialog(user);

          
            passwordChangeDialog.ShowDialog();

        }


        private string _selectedSortOption = "Sortează";

        public string SelectedSortOption
        {
            get { return _selectedSortOption; }
            set
            {
                _selectedSortOption = value;
                OnPropertyChange("optiuneSortare");
                ApplyFilterAndSort();
            }
        }

        private void ApplyFilterAndSort()
        {
            // Verifică dacă lista utilizatorilor este goală
            if (Users == null) Users = new ObservableCollection<User_>();

            using (var dbContext = new AuctionAppDataContext())
            {
                // Obține toți utilizatorii înainte de filtrare/sortare
                var query = dbContext.Users.AsQueryable();

                // Aplică sortarea și filtrarea pe baza opțiunii selectate
                switch (SelectedSortOption)
                {
                    case "Alfabetic după Username":
                        query = query.OrderBy(u => u.username);
                        break;

                    case "Alfabetic după Rol":
                        query = query.OrderBy(u => u.User_roles.OrderBy(r => r.id_tip).FirstOrDefault().id_tip);
                        break;

                    case "Afișează doar Administratori":
                        query = query.Where(u => u.User_roles.Any(r => r.id_tip == 1)); // Rolul 1 = Admin
                        break;

                    case "Afișează doar Clienți":
                        query = query.Where(u => u.User_roles.Any(r => r.id_tip == 3)); // Rolul 3 = Client
                        break;

                    case "Afișează doar Ofertanți":
                        query = query.Where(u => u.User_roles.Any(r => r.id_tip == 2)); // Rolul 2 = Ofertant
                        break;

                    default:
                        query = query.OrderBy(u => u.username); // Sortare implicită
                        break;
                }

                // Convertim rezultatele în lista utilizatorilor (sau listă goală dacă nu există rezultate)
                var usersList = query.ToList();

                // Actualizează lista Users
                Users = new ObservableCollection<User_>(usersList.Select(u => new User_(
                    id: u.id_user,
                    firstname: u.fisrt_name,
                    lastname: u.last_name,
                    username: u.username,
                    email: u.email,
                    date_created: u.created_at,
                    last_login: u.last_login,
                    balance: u.balance
                )));
            }
        }


        private void OnBack(User_ user)
        {
            var adminWindow = App.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
            var frame = adminWindow?.FindName("AdminFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_MainAdminPage());
            }
        }

        private ObservableCollection<User_> _users;

        public Vm_UsersDetailes()
        {
            LoadUsers();
            DeleteUser = new RelayCommand<User_>(OnDeleteUserPressed);
            BackCommand = new RelayCommand<User_>(OnBack);
            ChangeRole = new RelayCommand<User_>(OnChangeRolePressed);
            ViewProfile = new RelayCommand<User_>(OnViewProfilePressed);
        }



        private void OnDeleteUserPressed(User_ userToDelete)
        {
            if (userToDelete == null)
                return;

            bool isDeleted = DatabaseManager.DeleteUserById(userToDelete._id);

            if (isDeleted)
            {
                Users.Remove(userToDelete);
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Ștergerea utilizatorului a eșuat.");
            }
        }

        private void OnViewProfilePressed(User_ user)
        {
            if (user == null) return;

            var adminWindow = App.Current.Windows.OfType<AdminWindow>().FirstOrDefault();
            var frame = adminWindow?.FindName("AdminFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_AdminViewUserProfilePage(user));
            }
        }

        private void OnChangeRolePressed(User_ user)
        { 
            if (user == null) return;

            if ((new DatabaseManager()).GetRoleIdByUsername(user._username) == 3)
            {
                (new DatabaseManager()).UpdateUserRoleToBidder(user._username);
                LoadUsers();
            }
            else if ((new DatabaseManager()).GetRoleIdByUsername(user._username) == 2)
            {
                (new DatabaseManager()).UpdateUserRoleToClient(user._username); 
                LoadUsers();   
            }
            else
            {
                MessageBox.Show("Nu poți să renunți la dreptul de administrator!");
            }
        }


        public string GeneratePassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Username cannot be null or empty");
            }

            string password = $"{username}{12345678}";

            return password;
        }


        public ObservableCollection<User_> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChange(nameof(Users));
            }
        }

        private void LoadUsers()
        {
            using (var dbContext = new AuctionAppDataContext()) 
            {
                var usersList = dbContext.Users
                                         .Select(u => new
                                         {
                                             Id = u.id_user,
                                             FirstName = u.fisrt_name,
                                             LastName = u.last_name,
                                             Username = u.username,
                                             Email = u.email,
                                             Balance = u.balance
                                         })
                                         .ToList();

                Users = new ObservableCollection<User_>(usersList.Select(u => new User_(
                        id: u.Id,
                        firstname: u.FirstName,
                        lastname: u.LastName,
                        username: u.Username,
                        email: u.Email,
                        date_created: DateTime.Now,
                        last_login: DateTime.Now,
                        balance: u.Balance
                )));

            }

        }

    }
}
