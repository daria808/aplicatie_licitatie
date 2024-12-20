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
using Client_ADBD.ViewModels;

namespace Client_ADBD.Views
{
    /// <summary>
    /// Interaction logic for UsersDetailesPage.xaml
    /// </summary>
    public partial class UsersDetailesPage : Page
    {
        public UsersDetailesPage()
        {
            InitializeComponent();
            this.DataContext = new Vm_UsersDetailes();
        }
    }
}
