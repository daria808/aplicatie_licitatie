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

namespace Client_ADBD.Views
{
    /// <summary>
    /// Interaction logic for PostPage.xaml
    /// </summary>
    public partial class PostPage : Page
    {
        public PostPage(Post_ p, bool isAdmin = false)
        {
            InitializeComponent();
            VM_PostPage viewModel = new VM_PostPage(p, isAdmin);
            DataContext = viewModel;
        }

        public PostPage() { }
    }
}
