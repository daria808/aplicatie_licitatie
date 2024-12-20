using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.Models
{
    internal class Watch_:IProduct
    {
        public string Mechanism {  get; set; }
        decimal Diameter {  get; set; }
        string Manufacturer {  get; set; }
        string Type {  get; set; }

        public Watch_(string mechanism, decimal diameter, string manufacturer,int productId,string name,
                string description, DateTime ivnDate):base(productId,name, description,ivnDate)
        {
            Mechanism = mechanism;
            Diameter = diameter;
            Manufacturer = manufacturer;
            Type = name;
        }


    }
}
