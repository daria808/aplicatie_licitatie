using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.Models
{
    internal class Book_:IProduct
    {
        public string Condition {  get; set; }  
        public string Author {  get; set; }
        public int PublicationYear {  get; set; }
        public string PublishingHouse { get; set; }
        public int PageNumber {  get; set; }
        public string Language { get; set; }

        public Book_(int  productId,string name,string description, DateTime invDate,string condition, string author, 
            int publicationYear, string publishingHouse, int pageNumber, string language):base(productId, name,description,invDate)
        {
            Condition = condition;
            Author = author;
            PublicationYear = publicationYear;
            PublishingHouse = publishingHouse;
            PageNumber = pageNumber;
            Language = language;
        }
    }
}
