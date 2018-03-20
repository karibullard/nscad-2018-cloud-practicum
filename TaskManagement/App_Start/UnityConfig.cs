using System.Web.Http;
using API.DAL;
using TaskManagement.DAL;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace API
{
	public static class UnityConfig
	{
		public static void RegisterComponents()
		{
			var container = new UnityContainer();

			container.RegisterInstance(typeof(StorageContext), new StorageContext());
			container.RegisterType<IWorkflowRepository, WorkflowRepository>(new HierarchicalLifetimeManager());
			container.RegisterType<IUserRepositoryMongo, UserRepositoryMongo>();
			container.RegisterSingleton<IUserRepositoryMongo, UserRepositoryMongo>();
			GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
		}
	}
}