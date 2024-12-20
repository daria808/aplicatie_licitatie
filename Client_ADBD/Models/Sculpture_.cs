using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.Models
{
    internal class Sculpture_:IProduct
    {
        public string Material {  get; set; }
        public string Artist {  get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Depth { get; set; }

        public Sculpture_(int productId, string name, string description, DateTime invDate,
            string material, string artist, decimal length, decimal width, decimal depth) : base(productId, name, description, invDate)
        {
            Material = material;
            Artist = artist;
            Length = length;
            Width = width;
            Depth = depth;
        }
    }

}
