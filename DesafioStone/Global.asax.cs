using System.Web.Http;

namespace DesafioStone
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            App_Start.Bootstrapper.Run();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
