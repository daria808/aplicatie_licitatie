using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.Models
{
    internal class IProduct
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        DateTime InventoryDate { get; set; }

        public IProduct(int productID, string name, string description, DateTime inventoryDate)
        {
            ProductID = productID;
            Name = name;
            Description = description;
            InventoryDate = inventoryDate;
        }
    }

}
