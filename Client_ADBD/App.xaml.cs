using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO.Packaging;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Windows;

using Client_ADBD.Models;



namespace Client_ADBD
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            //List<Users> _users = DatabaseManager.GetUsers();

            base.OnStartup(e);

            
        }

    }
}
