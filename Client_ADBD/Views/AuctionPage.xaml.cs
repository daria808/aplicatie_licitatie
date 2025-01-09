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
using Client_ADBD.Models;

namespace Client_ADBD.Views
{
    /// <summary>
    /// Interaction logic for AuctionPage.xaml
    /// </summary>
    public partial class AuctionPage : Page
    {

        public AuctionPage()
        {
            InitializeComponent();
        }
        public AuctionPage(Auction_ a, bool FromResult = false, bool Statistics = false, bool admStats = false, bool isAdmin = false)
        {
            InitializeComponent();
            VM_AuctionPage viewModel = new VM_AuctionPage(a, FromResult, Statistics, admStats, isAdmin);
            DataContext = viewModel;

        }


    }
}
