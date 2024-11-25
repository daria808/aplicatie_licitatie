using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }  // Parola ar trebui să fie hash-uită înainte de a o salva.
        public string email { get; set; }
        public DateTime date_created { get; set; }
        public DateTime last_login { get; set; }
        public decimal balance { get; set; }
    }
}
