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
    /// Interaction logic for SculptureControl.xaml
    /// </summary>
    public partial class SculptureControl : System.Windows.Controls.UserControl
    {
        public SculptureControl()
        {
            InitializeComponent();
        }

        public SculptureControl(Sculpture_ s)
        {
            InitializeComponent();
            VM_SculpturePostControl viewModel = new VM_SculpturePostControl(s);
            DataContext=viewModel;

        }
    }
}
