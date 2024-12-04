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
            List<Auction_> auctions = new List<Auction_>
            {
                new Auction_
                {
                    id = 1,
                    name = "Licitația de Artă Istorică",
                    startTime = new DateTime(2024, 12, 11, 12, 10, 0),
                    endTime = new DateTime(2024, 12, 11, 19, 10, 0),
                    location = "Palatul Cesianu-Racoviță",
                    status = AuctionStatus.Ongoing,

                },
                new Auction_
                {
                    id = 2,
                    name = "Licitația de Mașini Clasice",
                    startTime = DateTime.Now.AddDays(1),
                    endTime = DateTime.Now.AddHours(3),
                    location = "Sala Palatului",
                    status = AuctionStatus.Upcoming
                }
            };

            VM_MainPage viewModel = new VM_MainPage();
            viewModel.SetAuctions(auctions);
            DataContext = viewModel;
        }



    }
}
