using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.Models
{
    public class Watch_:IProduct
    {
        public string Mechanism {  get; set; }
        public decimal Diameter {  get; set; }
         public string Manufacturer {  get; set; }
        public string Type {  get; set; }

        public Watch_(string mechanism, decimal diameter, string manufacturer,int productId,string name,decimal startPrice, decimal listPrice, string[]imagePaths,
                string description, DateTime ?invDate): base(productId, name, description, invDate, startPrice, listPrice, imagePaths)
        {
            Mechanism = mechanism;
            Diameter = diameter;
            Manufacturer = manufacturer;
            Type = name;
        }


    }
}
