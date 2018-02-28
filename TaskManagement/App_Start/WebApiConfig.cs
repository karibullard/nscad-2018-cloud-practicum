using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace API
{
	/// <summary>
	/// Injects Task Management API configuration settings at startup
	/// </summary>
	public static class WebApiConfig
	{
		/// <summary>
		/// Registers project configurations
		/// </summary>
		/// <param name="config">The configuration container</param>
		public static void Register(HttpConfiguration config)
		{
			config.Formatters.Add(new BsonMediaTypeFormatter());

			config.Formatters.JsonFormatter.SupportedMediaTypes
					.Add(new MediaTypeHeaderValue("text/html"));

			config.MapHttpAttributeRoutes();
			config.Routes.MapHttpRoute(
				"DefaultApi",
				"api/{controller}/{id}",
				new { id = RouteParameter.Optional });
		}
	}
}