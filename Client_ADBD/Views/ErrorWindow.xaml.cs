using Client_ADBD.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client_ADBD.Views
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        private VM_ErrorWindow viewModel;
        public ErrorWindow(string errorMessage)
        {
            InitializeComponent();
            viewModel=new VM_ErrorWindow(errorMessage);
            viewModel.CloseWindowAction=this.Close;
            DataContext = viewModel;

        }
        
    }
}
