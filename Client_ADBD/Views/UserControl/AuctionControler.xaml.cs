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
using Client_ADBD.ViewModels;

namespace Client_ADBD.Views.UserControl
{
    /// <summary>
    /// Interaction logic for AuctionControler.xaml
    /// </summary>
    public partial class AuctionControler : System.Windows.Controls.UserControl
    {
       // private VM_AuctionControler viewModel;
        public AuctionControler()
        {
            InitializeComponent();

            //viewModel = new ViewModels.VM_AuctionControler();
            //DataContext = viewModel;

        }

    }

}
