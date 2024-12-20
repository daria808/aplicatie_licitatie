using Client_ADBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.Helpers
{
    public class CurrentUser
    {
        private static User_ _user;

        public static User_ User
        {
            get => _user;
            set
            {
                _user = value;
            }
        }
        public static bool IsUserLoggedIn() => _user != null;
    }
}