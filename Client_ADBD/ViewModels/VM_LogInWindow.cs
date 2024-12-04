using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Client_ADBD.Helpers;
using Client_ADBD.Views;
using CommunityToolkit.Mvvm.Input;

namespace Client_ADBD.ViewModels
{
    internal class VM_LogInWindow:VM_Base
    {
        private string _username;
        private string _password;

        private string _usernameError;
        private string _passwordError;  


        private string _firstName;
        private string _lastName;
        private string _username2;
        private string _password2;
        private string _email;


        private string _firstNameError;
        private string _lastNameError;
        private string _usernameError2;    
        private string _passwordError2;
        private string _emailError;

        private string _sigInError;
        private string _sigUpError;

        public ICommand ChangeToSignInCommand { get; }
        public event EventHandler StoryBoardSI;

        public ICommand ChangeToSignUpCommand {  get; }
        public event EventHandler StoryBoardSU;

        public ICommand SignUpToSignInCommand { get; }
        public event EventHandler StoryBoardSI2;

        public ICommand SignInToSignUpCommand { get; }
        public event EventHandler StoryBoardSU2;
        public ICommand BackToStartWindow {  get; } 
        public ICommand SignIn {  get; }
        public ICommand SignUp { get; }
       public bool IsPasswordEmpty => string.IsNullOrEmpty(Password);
       public bool IsPasswordEmpty2 => string.IsNullOrEmpty(Password2);

        public event Action RequestPasswordReset;
        public VM_LogInWindow()
        {
           
            ChangeToSignInCommand = new RelayCommand(ChangeToSignIn);
            ChangeToSignUpCommand = new RelayCommand(ChangeToSignUp);
            SignUpToSignInCommand = new RelayCommand(ChangeToSignIn2);
            SignInToSignUpCommand = new RelayCommand(ChangeToSignUp2);
            BackToStartWindow = new RelayCommand(GoToStartWindow);
            SignIn = new RelayCommand(VerifyCredentials);
            SignUp = new RelayCommand(AddUser);
           
        }

        private void TriggerPasswordReset()
        {
            RequestPasswordReset?.Invoke();
        }
        private void AddUser()
        {
            bool areIncomplete = (_firstName == null || _lastName == null||_username2==null||_password2==null||_email==null);
            bool areErrors = ((!string.IsNullOrEmpty(FirstNameError))|| (!string.IsNullOrEmpty(LastNameError))  || (!string.IsNullOrEmpty(UsernameError2))|| (!string.IsNullOrEmpty(PasswordError2))|| (!string.IsNullOrEmpty(EmailError)));

            if (areIncomplete)
            {
                NavigationService.OpenWindow("ErrorWindow", "Câmpuri incomplete!");
            }
            else if (areErrors)
            {
                NavigationService.OpenWindow("ErrorWindow", "Date invalide!");
            }
            else
            {
                DatabaseManager.AddUser(FirstName, LastName, Username2, Password2, Email);
                NavigationService.NavigateTo("MainWindow");
            }
        }
        private void VerifyCredentials()
        {
            bool areIncomplete = (_username == null || _password == null);
            bool areErros = (!string.IsNullOrEmpty(UsernameError)|| !string.IsNullOrEmpty(PasswordError));
            
            if (areIncomplete)
            {
                NavigationService.OpenWindow("ErrorWindow", "Câmpuri incomplete!");
            }
            else if (areErros)
            {
                NavigationService.OpenWindow("ErrorWindow", "Date invalide!");
            }  
            else if(DatabaseManager.VerifyUserCredentials(Username,Password))
            {
                NavigationService.NavigateTo("MainWindow");
            }
            else
            {
                NavigationService.OpenWindow("ErrorWindow", "Credentiale invalide!");
            }

        }
        private void ChangeToSignIn()
        {
            StoryBoardSI?.Invoke(this, EventArgs.Empty);    
        }
        private void ChangeToSignUp()
        {
            StoryBoardSU?.Invoke(this, EventArgs.Empty);
        }
        private void ChangeToSignIn2()
        {
            Username = string.Empty;
            Password = string.Empty;
            UsernameError = string.Empty;
            PasswordError = string.Empty;

            TriggerPasswordReset();
      
            StoryBoardSI2?.Invoke(this, EventArgs.Empty);
        }
        private void ChangeToSignUp2()
        {
            FirstName = string.Empty;
            FirstNameError = string.Empty;  
            LastName = string.Empty;
            LastNameError = string.Empty;
            Username2 = string.Empty;
            UsernameError2 = string.Empty;
            Password2 = string.Empty;
            PasswordError2 = string.Empty;
            Email= string.Empty;
            EmailError = string.Empty;

            TriggerPasswordReset();
            
            StoryBoardSU2?.Invoke(this, EventArgs.Empty);
        }
        private void GoToStartWindow()
        {

            NavigationService.NavigateTo("StartWindow");
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                ValidateUsername();
                OnPropertyChange(nameof(Username));
            }
        }
        public string Password
        {
            get => _password;
            set
            {

                _password = value;
                ValidatePassword();
                OnPropertyChange(nameof(Password));
                OnPropertyChange(nameof(IsPasswordEmpty));
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                ValidateFisrtName();
                OnPropertyChange(nameof(FirstName));
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                ValidateLastName();
                OnPropertyChange(nameof(LastName));
            }
        }
        public string Username2
        {
            get => _username2;
            set
            {
                _username2 = value;
                ValidateUsername2();
                OnPropertyChange(nameof(Username2));
        
            }
        }
        public string Password2
        {
            get => _password2;
            set
            {
                _password2 = value;
                ValidatePassword2();
                OnPropertyChange(nameof(Password2));
                OnPropertyChange(nameof(IsPasswordEmpty2));
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                ValidateEmail();
                OnPropertyChange(nameof(Email));
            }
        }
        public string FirstNameError
        {
            get => _firstNameError;
            set
            {
                _firstNameError = value;
                OnPropertyChange(nameof(FirstNameError));
            }
        }
        public string LastNameError
        {
            get => _lastNameError;
            set
            {
                _lastNameError = value;
                OnPropertyChange(nameof(LastNameError));
            }
        }
        public string UsernameError
        {
            get => _usernameError;
            set
            {
                _usernameError = value;
                OnPropertyChange(nameof(UsernameError));
            }
        }
        public string PasswordError
        {
            get => _passwordError;
            set
            {
                _passwordError = value;
                OnPropertyChange(nameof(PasswordError));
            }
        }
        public string UsernameError2
        {
            get => _usernameError2;
            set
            {
                _usernameError2 = value;
                OnPropertyChange(nameof(UsernameError2));
            }
        }
        public string PasswordError2
        {
            get => _passwordError2;
            set
            {
                _passwordError2 = value;
                OnPropertyChange(nameof(PasswordError2));
            }
        }
        public string EmailError
        {
            get => _emailError;
            set
            {
                _emailError = value;
                OnPropertyChange(nameof(EmailError));
            }
        }

        void ValidateUsername()
        {
            // Validare username
            if (!validation.IsValidUsername(Username, out string usernameError))
            {
                UsernameError = usernameError;
            }
            else
            {
                UsernameError = string.Empty;
            }
        }

        void ValidatePassword()
        {
            // Validare parola
            if (!validation.IsValidPassword(Password, out string passwordError))
            {

                PasswordError = passwordError;
            }
            else
            {
                PasswordError = string.Empty;
            }
        }

        private void ValidateFisrtName()
        {
            if (!validation.IsValidName(FirstName, out string firstNameError))
            {
                FirstNameError = firstNameError;
            }
            else
            {
                FirstNameError = string.Empty;
            }
        }

        private void ValidateLastName() {
            if (!validation.IsValidName(LastName, out string lastNameError))
            {
                LastNameError = lastNameError;
            }
            else
            {
                LastNameError = string.Empty;
            }
        }

        private void ValidateUsername2()
        {
            if (!validation.IsValidUsername(Username2, out string usernameError2))
            {
                UsernameError2 = usernameError2;
            }
            else
            {
                UsernameError2 = string.Empty;
            }
        }

        private void ValidatePassword2()
        {

            if (!validation.IsValidPassword(Password2, out string passwordError2))
            {
                PasswordError2 = passwordError2;
            }
            else
            {
                PasswordError2 = string.Empty;
            }
        }

        private void ValidateEmail()
        {
            if (!validation.IsValidEmail(Email, out string emailError))
            {
                EmailError = emailError;
            }
            else
            {
                EmailError = string.Empty;
            }
        }
        
    }
}
