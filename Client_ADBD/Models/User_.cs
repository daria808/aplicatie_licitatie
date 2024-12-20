using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.Models
{
    public class User_
    {
        public int _id { get; set; }

        public string _firstName { get; set; }
        public string _lastName { get; set; }
        public string _username { get; set; }
        public string _email { get; set; }
        public DateTime? _dateCreated { get; set; }
        public DateTime? _lastLogin { get; set; }
        public decimal? _balance { get; set; }

        public bool _isAdmin { get; set; }
        public bool _isBidder { get; set; }
        public bool _isCustomer { get; set; }

        public string _role { get; set; }
        public User_(int id, string firstname, string lastname, string username, string email, DateTime? date_created, DateTime? last_login, decimal? balance)
        {
            _id = id;
            _firstName = firstname;
            _lastName = lastname;
            _username = username;
            _email = email;
            _dateCreated = date_created;
            _lastLogin = last_login;
            _balance = balance;

            int idUsername = (new DatabaseManager()).GetRoleIdByUsername(username);

            if (idUsername == 2)
            {
                _role = "ofertant";
                _isAdmin = false; _isBidder = true; _isCustomer = false;
            }
            else if (idUsername == 3)
            {
                _role = "client";
                _isAdmin = false; _isBidder = false; _isCustomer = true;
            }
            else if (idUsername == 1)
            {
                _role = "administrator";
                _isAdmin = true; _isBidder = false; _isCustomer = false;
            }
            else
            {
                _isAdmin = false; _isBidder = false; _isCustomer = true;
            }
        }
    }
}