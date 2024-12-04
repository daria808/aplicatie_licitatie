using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.Models
{
    public enum role
    {
        admin,
        bidder,
        customer
    }

    public class Users
    {
        public int _id { get; set; }

        public string _fistName { get; set; }
        public string _lastName { get; set; }
        public string _username { get; set; }
        public string _email { get; set; }
        public DateTime? _dateCreated { get; set; }
        public DateTime? _lastLogin { get; set; }
        public decimal? _balance { get; set; }

        bool _isAdmin { get; set; }
        bool _isBidder { get; set; }
        bool _isCustomer {  get; set; } 


        public Users(int id, string username, string email, DateTime? date_created, DateTime? last_login, decimal? balance)
        {
            _id = id;
            _username = username;
            _email = email;
            _dateCreated = date_created;
            _lastLogin = last_login;
            _balance = balance ;

            _isAdmin = false;
            _isBidder = false;
            _isCustomer = true;
        }


    }
}
