using HandyControl.Expression.Shapes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.Models
{


    public class Post_
    {
        public int postId { get; set; }
        public string productStatus { get; set; }
        public int auctionNumber { get; set; }
        public string auctionName {  get; set; }
        public string auctionType {  get; set; }
        public DateTime? creationTime { get; set; }

        public IProduct product;

        public int lotNumber {  get; set; }

        //public int lotNumber { get; set; }

        public Post_() { }
        public Post_(int postId, string productStatus, int auctionNumber, string auctionName, string auctionType, DateTime? creationTime,IProduct pr, int lotNumber)
        {
            this.postId = postId;
            this.productStatus = productStatus;
            this.auctionNumber = auctionNumber;
            this.auctionName = auctionName;
            this.auctionType = auctionType;
            this.creationTime = creationTime;
            this.lotNumber = lotNumber;

        }
    }
}
