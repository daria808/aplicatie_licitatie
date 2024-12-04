using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.Models
{
    public enum AuctionStatus
    {
        Upcoming, 
        Ongoing, 
        Closed    
    }


    internal class Auction_
    {

        public int id { get; set; } 
        public string name { get; set; } 
        public DateTime startTime { get; set; } 
        public DateTime endTime { get; set; } 
        public string location { get; set; }
        public AuctionStatus status { get; set; }
        public string statusStr => StatusToString(status);

       
        public static string StatusToString(AuctionStatus status)
        {
            switch (status)
            {
                case AuctionStatus.Upcoming:
                    return "Upcoming";
                case AuctionStatus.Ongoing:
                    return "Ongoing";
                case AuctionStatus.Closed:
                    return "Closed";
                default:
                    return "Unknown";
            }
        }


    }
}
