using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Client_ADBD.Helpers;

namespace Client_ADBD
{
    internal class DatabaseManager
    {
        private static AuctionAppDataContext _dbContext;
        public DatabaseManager()
        {
            _dbContext = new AuctionAppDataContext();
        }

        static public void PrintUsers()
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var users = from u in _dbContext.Users select u;
            foreach (var user in users)
            {
                Console.WriteLine(user.username);


            }
        }

        static public void AddUser(string firstName,string lastName,string username,string password,string email)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var salt = Hash.GenerateSalt();
            var hashPassword = Hash.HashPassword(password, salt);

            var newUser = new User
            {
                fisrt_name= firstName,
                last_name = lastName,
                username = username,
                salt = salt,
                password = hashPassword,
                email = email,
                created_at = DateTime.Now, 
                last_login = null,
                balance = 0.00M 
            };

            _dbContext.Users.InsertOnSubmit(newUser);
            _dbContext.SubmitChanges(); 
        }

        public static bool VerifyUserCredentials(string username, string password)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var user = (from u in _dbContext.Users
                        where u.username == username
                        select new { u.username, u.password, u.salt }).FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            return Hash.VerifyPassword(password, user.password, user.salt);
        }
    }
}
