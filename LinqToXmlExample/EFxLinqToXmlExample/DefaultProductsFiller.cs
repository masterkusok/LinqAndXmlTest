using Microsoft.EntityFrameworkCore;

namespace EFxLinqToXmlExample
{
    public class DefaultProductsFiller
    {
        public static void FillDbWithDefaultProducts()
        {
            using(ProductContext db = new ProductContext())
            {
                Product pc = new Product() { Name = "Gaming Pc", Type = "electronics", Price = 1000, ImageUrl = "pc.png" };
                Product burger = new Product() { Name = "Burger", Type = "food", Price = 5, ImageUrl = "burger.jpg" };
                Product boots = new Product() { Name = "Prada Boots", Type = "fashion", Price = 300, ImageUrl = "boots.jpg" };
                Product fan = new Product() { Name = "Dyson Fan", Type = "electronics", Price = 350, ImageUrl = "fan.png" };
                Product cola = new Product() { Name = "Coca Cola", Type = "food", Price = 2, ImageUrl = "colacoca.jpg" };
                Product hm = new Product() { Name = "Men sweatshirt", Type = "fashion", Price = 15, ImageUrl = "hmsweatshirt.jpg" };

                db.AddRange(pc, burger, boots, fan, cola, hm);
                db.SaveChanges();
            }
        }
    }
}
