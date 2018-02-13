using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;

namespace API {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            
            config.Formatters.Add(new BsonMediaTypeFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();
            // XML to Json Format
            config.Formatters.JsonFormatter.SupportedMediaTypes
            .Add(new MediaTypeHeaderValue("text/html"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
