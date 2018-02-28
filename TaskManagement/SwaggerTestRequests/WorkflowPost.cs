using API.Models;
using Swashbuckle.Examples;

namespace API.SwaggerTestRequests
{
	/// <summary>
	/// Populates JSON request examples for Workflow Post resouce in Swagger UI
	/// </summary>
	public class WorkflowPost : IExamplesProvider
	{
		/// <summary>
		/// Populates model values for SwaggeUI
		/// </summary>
		/// <returns>Workflow model as JSON</returns>
		public object GetExamples()
		{
			return new Workflow
			{
				Name = "CloudOffshoreExternal",
				Description = "Onboarding tasks for offshore external Cloud employees.",
				Tasks = Task.DCDOffshoreExternal
			};
		}
	}
}