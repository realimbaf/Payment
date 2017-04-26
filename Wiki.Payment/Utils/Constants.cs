using Wiki.Service.Configuration;

namespace Wiki.Payment.Utils
{
    public class Constants
    {
        public static string CONNECTIONSTRING = ConfigurationContainer.Configuration["ConnectionString"];
    }
}
