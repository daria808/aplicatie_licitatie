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


    public class Auction_
    {
        public static readonly string DEFAULT_IMAGE_PATH = @"C:\Users\laris\Desktop\Client_ADBD\Client_ADBD\Views\photos\no-image_image.jpg";
        public int id { get; set; } 

        public int auctionNumber {  get; set; } 
        public string name { get; set; } 
        public DateTime startTime { get; set; } 
        public DateTime endTime { get; set; } 
        public string location { get; set; }
        public AuctionStatus status { get; set; }
        public string statusStr => StatusToString(status);
        public string imagePath { get; set; }
        public string description { get; set; }

        public string usernameOwner {  get; set; }  
        public string auctionType {  get; set; }

        public Auction_() { }
        public Auction_(Auction a)
        {
            id=a.id_auction;
            name = a.name;
            startTime=a.start_time;
            endTime=a.end_time;
            location = a.location;
            imagePath=a.image_path;
            description = a.description;
            auctionNumber=a.auction_number;
            usernameOwner = DatabaseManager.GetUsernameById(a.id_user);
            auctionType = DatabaseManager.GetAuctionType(a.id_auction_type);
        }
       
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
