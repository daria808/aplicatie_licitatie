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

namespace Client_ADBD.Views
{
    /// <summary>
    /// Interaction logic for AddPostPage.xaml
    /// </summary>
    public partial class AddPostPage : Page
    {

        public AddPostPage()
        {

            InitializeComponent();
        }

        public AddPostPage(string postType, int auctionNumber, bool isAdmin = false)
        {
            InitializeComponent();
            VM_AddPost viewModel = new VM_AddPost(postType, auctionNumber, isAdmin);
            //VM_AddPost viewModel = new VM_AddPost("Sculpturi",1);
            DataContext = viewModel;

        }
    }
}
