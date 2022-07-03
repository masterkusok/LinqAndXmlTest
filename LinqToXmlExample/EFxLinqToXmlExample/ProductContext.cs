using Microsoft.EntityFrameworkCore;

namespace EFxLinqToXmlExample
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionStringManager.GetConfigurationString(), new MySqlServerVersion(new Version(8, 0)));
        }
    }
}
