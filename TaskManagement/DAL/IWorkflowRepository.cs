using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.Responses;

namespace API.DAL
{
	/// <summary>
	/// An interface for WorkflowRepository.
	/// </summary>
	public interface IWorkflowRepository
	{
		/// <summary>
		/// List all workflows asynchronously
		/// </summary>
		/// <returns>A list of all workflows</returns>
		Task<List<WorkflowsGet>> ListAllAsync();

		/// <summary>
		/// List all workflows
		/// </summary>
		/// <returns>A list of all workflows</returns>
		List<WorkflowsGet> ListAll();

		/// <summary>
		/// Gets a workflow configuration by id
		/// </summary>
		/// <param name="id">The id of the workflow to retrieve</param>
		/// <returns>A workflow object</returns>
		Task<Workflow> GetAsync(string id);

		/// <summary>
		/// Get specific workflows
		/// </summary>
		/// <param name="id">Id of workflows</param>
		/// <returns>A workflow</returns>
		Workflow Get(string id);

		/// <summary>
		/// Add a workflow asynchronously
		/// </summary>
		/// <param name="item">Workflow item to be added</param>
		/// <returns>A boolean wether or not it was added</returns>
		Task<Workflow> AddAsync(Workflow item);

		/// <summary>
		/// Add a workflow
		/// </summary>
		/// <param name="item">Workflow item to be added</param>
		/// <returns>A boolean wether or not it was added</returns>
		Workflow Add(Workflow item);

		/// <summary>
		/// Update a workflow
		/// </summary>
		/// <param name="item">A workflow item to be updated</param>
		/// <returns>A bool wether or not it was updated</returns>
		Task<Workflow> UpdateAsync(Workflow item);

		/// <summary>
		/// Update a workflow
		/// </summary>
		/// <param name="item">A workflow item to be updated</param>
		/// <returns>A bool wether or not it was updated</returns>
		Workflow Update(Workflow item);

		/// <summary>
		/// Delete a workflow asynchronously
		/// </summary>
		/// <param name="id">string to delete</param>
		/// <returns>A bool wether or not delete was successful</returns>
		Task<bool> DeleteAsync(string id);

		/// <summary>
		/// Delete a workflow
		/// </summary>
		/// <param name="id">string to delete</param>
		/// <returns>A bool wether or not delete was successful</returns>
		bool Delete(string id);
	}
}