using Client_ADBD.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client_ADBD.Views.UserControl
{
    /// <summary>
    /// Interaction logic for BookPostControl.xaml
    /// </summary>
    public partial class BookPostControl : System.Windows.Controls.UserControl
    {
        public BookPostControl()
        {
            InitializeComponent();
        }

        public BookPostControl(Book_ b)
        {
            InitializeComponent();
            VM_BookPostControl viewModel = new VM_BookPostControl(b);
            DataContext=viewModel;

        }
    }
}
