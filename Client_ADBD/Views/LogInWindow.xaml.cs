using Client_ADBD.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client_ADBD.Helpers;
using HandyControl.Controls;

namespace Client_ADBD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LogInWindow : System.Windows.Window    
    {
        private VM_LogInWindow viewModel;
        public LogInWindow()
        {
            InitializeComponent();
            viewModel= new VM_LogInWindow();
            DataContext = viewModel;

            viewModel.RequestPasswordReset += ResetPasswordBoxes;

            HandleStoryboardTransitions();
        }

        private void HandleStoryboardTransitions()
        {
            viewModel.StoryBoardSI += (sender, e) => TriggerStoryboard("AccesToSignIn");
            viewModel.StoryBoardSU += (sender, e) => TriggerStoryboard("AccesToSignUp");
            viewModel.StoryBoardSI2 += (sender, e) => TriggerStoryboard("SignUpToSignIn");
            viewModel.StoryBoardSU2 += (sender, e) => TriggerStoryboard("SignInToSignUp");
        }
        private void TriggerStoryboard(string storyboardName)
        {
            if (FindResource(storyboardName) is Storyboard storyboard)
            {
                storyboard.Begin();
            }
            else
            {
                Debug.WriteLine($"Storyboard '{storyboardName}' not found.");
            }
        }
        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
         
            if (this.DataContext != null)
            {
                var passwordBox = (System.Windows.Controls.PasswordBox)sender;  // Correct custom control casting
                ((dynamic)this.DataContext).Password = passwordBox.Password;
               // ((dynamic)this.DataContext).Password = ((hc:PasswordBox)sender).Password;
            }

        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                var passwordBox = (System.Windows.Controls.PasswordBox)sender;

                // Actualizează parola în timp real în ViewModel
                ((dynamic)this.DataContext).Password = passwordBox.Password;


            }
        }
        private void PasswordBox_LostFocus2(object sender, RoutedEventArgs e)
        {

            if (this.DataContext != null)
            {
                var passwordBox = (System.Windows.Controls.PasswordBox)sender;  // Correct custom control casting
                ((dynamic)this.DataContext).Password2 = passwordBox.Password;
                // ((dynamic)this.DataContext).Password = ((hc:PasswordBox)sender).Password;
            }
        }
        private void PasswordBox_PasswordChanged2(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                var passwordBox = ( System.Windows.Controls.PasswordBox)sender;

                ((dynamic)this.DataContext).Password2 = passwordBox.Password;


            }
        }
        private void ResetPasswordBoxes()
        {
            lg_tbPassword.Password = string.Empty;
            lg_tbPassword.Password = string.Empty; 
        }

    }
}
