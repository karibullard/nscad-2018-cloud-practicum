using System.Web.Http;
using Unity;
using Unity.WebApi;
using TaskManagement.DAL;

namespace TaskMangement
{
    /// <summary>
    /// UnityConfig for Dependency Injection
    /// </summary>
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterSingleton<IUserRepository, UserRepository>();

        }
    }
}