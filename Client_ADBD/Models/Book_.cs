using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.Models
{
    public class Book_:IProduct
    {
        public string Condition {  get; set; }  
        public string Author {  get; set; }
        public int PublicationYear {  get; set; }
        public string PublishingHouse { get; set; }
        public int PageNumber {  get; set; }
        public string Language { get; set; }

        public Book_(int  productId,string name,string description, DateTime invDate,decimal startPrice,decimal listPrice,string condition, string author, 
            int publicationYear, string publishingHouse, int pageNumber, string language, string[] imagePaths) :base(productId, name,description,invDate,startPrice,listPrice,imagePaths)
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
