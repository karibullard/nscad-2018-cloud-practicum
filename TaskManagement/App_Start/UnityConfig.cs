using System.Web.Http;
using API.DAL;
using TaskManagement.DAL;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace TaskMangement
{
	/// <summary>
	/// Ioc container configurations
	/// </summary>
	public static class UnityConfig
	{
		/// <summary>
		/// Registers the required components with the container
		/// </summary>
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