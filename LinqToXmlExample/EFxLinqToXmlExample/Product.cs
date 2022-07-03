using System.ComponentModel.DataAnnotations;

namespace EFxLinqToXmlExample
{
    public class Product
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        [Key]
        public int ProductID { get; set; }
        public string ImageUrl { get; set; }


        public Product Clone()
        {
            return this.MemberwiseClone() as Product;
        }
    }
}
