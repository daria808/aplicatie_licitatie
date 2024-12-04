using HandyControl.Expression.Shapes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.Models
{
    internal class Post
    {
        public int postId { get; set; }
        public string productId {  get; set; }
        public string productStatus { get; set; }
        public string auctionName { get; set; }

   //     [id_post] INT IDENTITY(1, 1) NOT NULL,

   //[id_user]     INT NULL,

   //[id_product]  INT NULL,

   //[id_status]   INT NULL,

   //[id_auction]  INT NULL,

   //[start_price] DECIMAL(10, 2) NOT NULL,

   //[list_price]  DECIMAL(10, 2) NOT NULL,

   //[created_at]  DATETIME DEFAULT(getdate()) NULL,
   // PRIMARY KEY CLUSTERED([id_post] ASC),
   // FOREIGN KEY([id_user]) REFERENCES[dbo].[Users] ([id_user]),
   // FOREIGN KEY([id_product]) REFERENCES[dbo].[Product] ([id_product]),
   // FOREIGN KEY([id_status]) REFERENCES[dbo].[Post status] ([id_status]),
   // FOREIGN KEY([id_auction]) REFERENCES[dbo].[Auction] ([id_auction]),
   // CONSTRAINT[CK_PostCreationDate] CHECK([created_at]<=getdate())


    }
}
