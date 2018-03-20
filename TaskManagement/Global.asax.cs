using System.Web.Http;
using System.Web.Routing;

namespace API
{
	/// <summary>
	/// App_start
	/// </summary>
	public class WebApiApplication : System.Web.HttpApplication
	{
		/// <summary>
		/// App_start properties Registers UnityConfig
		/// </summary>
		protected void Application_Start()
		{
			RouteTable.Routes.RouteExistingFiles = true;
			UnityConfig.RegisterComponents();
			GlobalConfiguration.Configure(WebApiConfig.Register);
		}
	}
}