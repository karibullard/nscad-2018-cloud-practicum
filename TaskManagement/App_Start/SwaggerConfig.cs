using System.Web.Http;
using API;
using Swashbuckle.Application;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]
namespace API
{
    /// <summary>
    /// Configuration settings for Swagger UI
    /// </summary>
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "API");
                        c.PrettyPrint();
                        c.IncludeXmlComments(string.Format(@"{0}\bin\API.xml", System.AppDomain.CurrentDomain.BaseDirectory));
                        c.DescribeAllEnumsAsStrings();
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.DocumentTitle("UST Onboarding Task Management API");
                    });
        }
    }
}
