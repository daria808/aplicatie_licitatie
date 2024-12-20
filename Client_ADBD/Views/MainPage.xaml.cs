using Client_ADBD.Models;
using Client_ADBD.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Linq;
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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            VM_MainPage viewModel = new VM_MainPage();
            DataContext = viewModel;
            List<Auction_> auctions = DatabaseManager.GetAuction(viewModel.Status,viewModel.SelectedSortOption);
            viewModel.SetAuctions(auctions);

          

        }

        
    }
}
