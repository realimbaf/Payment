using System.Web.Http;
using Owin;
using Wiki.Payment.Utils;
using Wiki.Service.Configuration;

namespace Wiki.Payment
{
    public class ServiceConfig :IServiceConfig
    {
        public void Init(IAppBuilder app, HttpConfiguration config)
        {
            AutoMapperWebConfiguration.Configure();
        }

        public void Start()
        {
            
        }

        public void Stop()
        {
            
        }
    }
}
