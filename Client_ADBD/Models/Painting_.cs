using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.Models
{
    public class Painting_:IProduct
    {
        public string Type { get; set; }
        public string Artist { get; set; }
        public int CreationYear { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
     

        public Painting_(int productId, string name, string description, DateTime? invDate, decimal startPrice, decimal listPrice, string type, string artist,
            int creationYear, decimal length,decimal width, string[] imagePaths) : base(productId, name, description, invDate, startPrice, listPrice, imagePaths)
        {
            Type = type;
            Artist = artist;
            CreationYear = creationYear;
            Length = length;
            Width = width;
        }
    }
}
