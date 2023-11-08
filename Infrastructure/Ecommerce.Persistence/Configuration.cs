using Microsoft.Extensions.Configuration;

namespace Ecommerce.Persistence;

public  static class   Configuration
{
    public static string ConnectionString
    {
        get
        {
            ConfigurationManager configurationManager = new ConfigurationManager();
            configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory() , "../../Presentation/Ecommerce.API"));
            configurationManager.AddJsonFile("appsettings.json"); 
            return configurationManager.GetConnectionString("Default");
        }
    }
}