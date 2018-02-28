using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.Responses;

namespace API.DAL
{
	public interface IWorkflowRepository
	{
		Task<IList<WorkflowsGet>> ListAll();

		/// <summary>
		/// Gets a workflow configuration by id
		/// </summary>
		/// <param name="id">The id of the workflow to retrieve</param>
		/// <returns>A workflow object</returns>
		Task<Workflow> GetAsync(string id);

		Workflow Get(string id);

		Task<bool> Add(Workflow item);

		Task<bool> Update(Workflow item);

		Task<bool> Delete(string id);
	}
}