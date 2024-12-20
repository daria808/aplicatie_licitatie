using HandyControl.Expression.Shapes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.Models
{


    internal class IPost
    {
        public int postId { get; set; }
        public int? productId { get; set; }
        public string productStatus { get; set; }
        public string auctionName { get; set; }
        public decimal startPrice { get; set; }
        public decimal listPrice { get; set; }
        public string imagePath { get; set; }
        public DateTime? creationTime { get; set; }


    }
}
