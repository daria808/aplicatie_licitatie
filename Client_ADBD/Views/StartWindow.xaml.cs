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
using System.Windows.Shapes;

using Client_ADBD.Helpers;

namespace Client_ADBD
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public static bool IsAppStart = true;

        public StartWindow()
        {
            InitializeComponent();

             if(IsAppStart)
             {
               NavigationService.ActiveWindow = this;
                IsAppStart = false;
             }

            DataContext = new VM_StartWindow();    
        }
    }
}
