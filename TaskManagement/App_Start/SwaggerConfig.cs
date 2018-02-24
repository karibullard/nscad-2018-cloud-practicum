using System.Web;
using System.Web.Http;
using API;
using Swashbuckle.Application;
using Swashbuckle.Examples;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]
namespace API
{
    /// <summary>
    /// Configuration settings for Swagger UI
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// Registers swagger configurations with web api
        /// </summary>
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
                        c.OperationFilter<ExamplesOperationFilter>();
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.DocumentTitle("UST Onboarding");
                    });
        }
    }
}
