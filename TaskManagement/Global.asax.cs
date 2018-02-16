namespace API
{
    using System.Web.Http;
    using System.Web.Routing;
    using TaskMangement;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            RouteTable.Routes.RouteExistingFiles = true;

            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Added UnityConfig for Dependency Injection.
            UnityConfig.RegisterComponents();

        }
    }
}
