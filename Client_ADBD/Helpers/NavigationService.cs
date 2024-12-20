using Client_ADBD.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace Client_ADBD.Helpers
{
    public class NavigationService
    {
        public static Window ActiveWindow { get; set; }
        public static Window PrevWindow { get; set; }   
     
        public static void NavigateTo( string windowName)
        {
          
            if (ActiveWindow != null)
            {

                Window newWindow = CreateWindow(windowName);

                if (newWindow != null)
                {
                    PrevWindow = ActiveWindow;
                    ActiveWindow = newWindow;
                    ActiveWindow.Show();
                    ActiveWindow.Activate();
                    PrevWindow.Close();
                }
            }

          
        }
        public static void OpenWindow(string windowName,string errorMessage=null)
        {
            Window newWindow = CreateWindow(windowName,errorMessage);

            if (newWindow != null)
            {
                newWindow.Owner = ActiveWindow;
                newWindow.Show();
                newWindow.Activate();
            }
        }

        private static Window CreateWindow(string windowName,string errorMessage = null)
        {
            switch (windowName)
            {
                case "StartWindow":
                    return new StartWindow();
                case "LogInWindow":
                    return new LogInWindow();
                case "ErrorWindow":
                    if(errorMessage!=null)
                    {
                        return new ErrorWindow(errorMessage);
                    }
                    return null;

                case "AdminWindow":
                    return new AdminWindow();
                case "MainWindow":
                    return new MainWindow();
                default:
                    return null;
            }
        }
    }
}
