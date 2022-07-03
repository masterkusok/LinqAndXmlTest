using Microsoft.Extensions.Configuration;

namespace EFxLinqToXmlExample
{
    public class ConnectionStringManager
    {
        public static string GetConfigurationString()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("config.json");
            var config = builder.Build();
            return config.GetConnectionString("DefaultConnection");
        }
    }
}
