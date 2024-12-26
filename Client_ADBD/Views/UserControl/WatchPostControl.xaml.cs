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
using Client_ADBD.Models;
using Client_ADBD.ViewModels;

namespace Client_ADBD.Views.UserControl
{
    /// <summary>
    /// Interaction logic for WatchPostControl.xaml
    /// </summary>
    public partial class WatchPostControl : System.Windows.Controls.UserControl 
    {
        public WatchPostControl() { }
        public WatchPostControl(Watch_ w)
        {
            InitializeComponent();
            VM_WatchPostControl viewModel = new VM_WatchPostControl(w);
            DataContext = viewModel;
        }
    }
}
