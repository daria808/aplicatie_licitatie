﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Client_ADBD.Models
{
    internal class Jewelry_:IProduct
    {
        public string Type {  get; set; }
        public string Brand {  get; set; }
        public decimal Weight {  get; set; }
        public int CreationYear {  get; set; }
        public Jewelry_(int productId, string name, string description, DateTime invDate,
            string type, string brand, decimal weight, int creationYear) : base(productId, name, description, invDate)
        {
            Type = type;
            Brand = brand;
            Weight = weight;
            CreationYear = creationYear;
        }
    }
}
