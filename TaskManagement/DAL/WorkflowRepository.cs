using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using API.Models;
using API.Responses;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json.Linq;

namespace API.DAL
{
	/// <summary>
	/// Provides Workflow CRUD opertaions
	/// </summary>
	public class WorkflowRepository : IWorkflowRepository, IDisposable
	{
		private readonly string[] workflowNames = { "Cloud", "DCDO", "Dev9", "Logic2020", "SharePoint" };
		private readonly string[] workflowTypes = { "OffshoreExternal", "OnsiteExternal", "OffshoreInternal", "OnsiteInternal" };
		private readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
		private IList<WorkflowsGet> workflowDtos;
		private IList<Workflow> workflows;
		private StorageContext context;
		private bool disposed;

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkflowRepository"/> class.
		/// </summary>
		/// <param name="context">Provides access ti Azure storage account</param>
		public WorkflowRepository(StorageContext context)
		{
			this.context = context;
			disposed = false;
		}

		internal IList<WorkflowsGet> WorkflowDtos
		{
			get
			{
				if (workflowDtos == null)
				{
					workflowDtos = context.GetDtoWorkflows().Result;
				}

				return workflowDtos;
			}

			set
			{
				this.workflowDtos = value;
			}
		}

		internal IList<Workflow> Workflows
		{
			get
			{
				if (workflows == null)
				{
					workflows = context.GetWorkflows();
				}

				return workflows;
			}

			set
			{
				this.workflows = value;
			}
		}

		/// <summary>
		/// Gets a workflow from in-memory collection
		/// </summary>
		/// <param name="id">The workflow id</param>
		/// <returns>A workflow object</returns>
		public Workflow Get(string id)
		{
			var workflow = Workflows.Cast<Workflow>().FirstOrDefault(x => x.Id.Equals(id));
			return workflow;

			throw new ArgumentNullException(nameof(id));
		}

		/// <summary>
		/// Gets a workflow from Azure storage by id
		/// </summary>
		/// <param name="id">The id of the workflow to retrieve</param>
		/// <returns>A workflow object</returns>
		public async Task<Workflow> GetAsync(string id)
		{
			var container = context.GetContainer();
			var blob = container.GetBlockBlobReference(id);
			byte[] blobBytes = new byte[0];
			var cloudBlobExists = await blob.ExistsAsync();
			if (cloudBlobExists)
			{
				Stream blobStream = await blob.OpenReadAsync();
				blobStream.Position = 0;
				var rawJson = new StreamReader(blobStream).ReadToEnd();
				var json = JObject.Parse(rawJson);
				var workflow = new Workflow();
				workflow.Id = json["Id"].ToString();
				workflow.Name = json["Name"].ToString();
				workflow.Description = json["Description"].ToString();
				workflow.Tasks = json["Tasks"].ToObject<List<Models.Task>>();
				return workflow;
			}

			return null;
		}

		/// <summary>
		/// Adds a workflow configuration to blob container
		/// </summary>
		/// <param name="item">The workflow item to add to storage</param>
		/// <returns>The added workflow</returns>
		public async Task<bool> Add(Workflow item)
		{
			var result = await context.UploadAsJson(item);
			if (result != null)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Updates a workflow configuration
		/// </summary>
		/// <param name="item">The workflow to update</param>
		/// <returns>True if operation was successful, otherwise false</returns>
		public async Task<bool> Update(Workflow item)
		{
			//var workflow = Workflows.FirstOrDefault(x => x.Id == item.Id);
			//if (workflow == null)
			//{
			//	return false;
			//}

			var result = await context.UploadAsJson(item);
			if (result != null)
			{
				return true;
			}

			return false;
		}

		private void AddToLocalWorkflows(Workflow item)
		{
			AddLocalWorkflow(item);
			AddLocalWorkflowDto(item);
		}

		private void AddLocalWorkflow(Workflow item)
		{
			Workflows.Add(item);
		}

		private void AddLocalWorkflowDto(Workflow item)
		{
			var dto = new WorkflowsGet(item.Name, item.Id);
			WorkflowDtos.Add(dto);
		}

		/// <summary>
		/// Deletes a workflow from the collection via the workflow object
		/// </summary>
		/// <param name="id">The id of the workflow to remove</param>
		/// <returns>True if operation was successful, otherwise false.</returns>
		public async Task<bool> Delete(string id)
		{
			return await context.DeleteAsync(id);
		}

		/// <summary>
		/// Returns all workflow dtos
		/// </summary>
		/// <returns>All workflows from storage</returns>
		public async Task<IList<WorkflowsGet>> ListAll()
		{
			return WorkflowDtos;
		}

		/// <summary>
		/// Reads the workflow configurations from a resource file
		/// </summary>
		/// <remarks>The file upload will create or overwrite the existing</remarks>
		public void SeedWorkflows()
		{
			for (int i = 0; i < 5; i++)
			{
				var currentWorkflow = workflowNames[i];

				for (int j = 0; i < 4; i++)
				{
					var workflowName = currentWorkflow + workflowTypes[j];
					List<Models.Task> taskList = null;
					switch (currentWorkflow)
					{
						case "Cloud":
							if (j == 0)
							{
								taskList = Models.Task.CloudOffshoreExternal;
							}
							else if (j == 1)
							{
								taskList = Models.Task.CloudOnsiteExternal;
							}
							else if (j == 2)
							{
								taskList = Models.Task.CloudOffshoreInternal;
							}
							else if (j == 3)
							{
								taskList = Models.Task.CloudOnsiteInternal;
							}

							break;

						case "DCDO":
							if (j == 0)
							{
								taskList = Models.Task.DCDOffshoreExternal;
							}
							else if (j == 1)
							{
								taskList = Models.Task.DCDOnsiteExternal;
							}
							else if (j == 2)
							{
								taskList = Models.Task.DCDOffshoreInternal;
							}
							else if (j == 3)
							{
								taskList = Models.Task.DCDOnsiteInternal;
							}

							break;

						case "Dev9":
							if (j == 0)
							{
								taskList = Models.Task.Dev9OffshoreExternal;
							}
							else if (j == 1)
							{
								taskList = Models.Task.DCDOnsiteExternal;
							}
							else if (j == 2)
							{
								taskList = Models.Task.Dev9OffshoreInternal;
							}
							else if (j == 3)
							{
								taskList = Models.Task.Dev9OnsiteInternal;
							}

							break;

						case "Logic2020":
							if (j == 0)
							{
								taskList = Models.Task.Logic2020OffshoreExternal;
							}
							else if (j == 1)
							{
								taskList = Models.Task.Logic2020OnsiteExternal;
							}
							else if (j == 2)
							{
								taskList = Models.Task.Logic2020OffshoreInternal;
							}
							else if (j == 3)
							{
								taskList = Models.Task.Logic2020OnsiteInternal;
							}

							break;

						case "SharePoint":
							if (j == 0)
							{
								taskList = Models.Task.SharePointOffshoreExternal;
							}
							else if (j == 1)
							{
								taskList = Models.Task.SharePointOnsiteExternal;
							}
							else if (j == 2)
							{
								taskList = Models.Task.SharePointOffshoreInternal;
							}
							else if (j == 3)
							{
								taskList = Models.Task.SharePointOnsiteInternal;
							}

							break;
					}

					var workflowConfig = new Workflow()
					{
						Name = workflowName,
						Tasks = taskList
					};

					Workflows.Add(workflowConfig);
				}
			}
		}

		/// <summary>
		/// Public implementation of Dispose pattern callable by consumers.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
		}

		/// <summary>
		/// Protected IDispose implementation
		/// </summary>
		/// <param name="disposing">Indicates if item is being disposed or not</param>
		protected void Dispose(bool disposing)
		{
			if (disposed)
			{
				return;
			}

			if (disposing)
			{
				handle.Dispose();
			}

			disposed = true;
		}
	}
}