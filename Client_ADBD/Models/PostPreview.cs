using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.Models
{
    internal class PostPreview
    {
        public int postId {  get; set; }    
        public string imagePath {  get; set; }
        public string postName {  get; set; }
        public string artistName {  get; set; }
        public decimal startPrice {  get; set; }
        public string postStatus {  get; set; }

    }
}
