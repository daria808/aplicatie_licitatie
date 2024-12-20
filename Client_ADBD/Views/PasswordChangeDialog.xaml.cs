using Client_ADBD.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Client_ADBD.Views
{
    public partial class PasswordChangeDialog : Window
    {
        public PasswordChangeDialog()
        {
            InitializeComponent();
        }

        private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is VM_PasswordChangeDialog viewModel)
            {
                viewModel.NewPassword = ((PasswordBox)sender).Password;
            }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is VM_PasswordChangeDialog viewModel)
            {
                viewModel.ConfirmPassword = ((PasswordBox)sender).Password;
            }
        }
    }
}