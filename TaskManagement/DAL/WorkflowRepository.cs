using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using API.Controllers;
using API.Models;
using API.Responses;
using Microsoft.Win32.SafeHandles;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API.DAL
{
	/// <summary>
	/// Provides Workflow CRUD opertaions
	/// </summary>
	public class WorkflowRepository : IWorkflowRepository, IDisposable
	{
		private const string CONTENTTYPE = "application/json";
		private readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
		private StorageContext context;
		private CloudBlobContainer cloudBlobContainer;
		private List<Workflow> workflows;
		private bool disposed;

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkflowRepository"/> class.
		/// </summary>
		/// <param name="context">Provides access ti Azure storage account</param>
		public WorkflowRepository(StorageContext context)
		{
			this.context = context;
			this.cloudBlobContainer = context.GetBlobContainer();
			this.disposed = false;
			this.workflows = new List<Workflow>();
			var results = cloudBlobContainer.ListBlobs();
			foreach (IListBlobItem item in results)
			{
				// Reads JSON From Blob Stream
				CloudBlob blob = cloudBlobContainer.GetBlobReference(item.Uri.Segments.Last());
				Stream blobStream = blob.OpenRead();
				blobStream.Position = 0;
				var rawJson = new StreamReader(blobStream).ReadToEnd();

				// Converts raw JSON to workflow instance
				JObject json = JObject.Parse(rawJson);
				var workflow = json.ToObject<Workflow>();
				workflows.Add(workflow);
			}
		}

		/// <summary>
		/// Gets a workflow configuration by id
		/// </summary>
		/// <param name="id">The workflow id</param>
		/// <returns>A workflow object</returns>
		public Workflow Get(string id)
		{
			var workflow = workflows.Cast<Workflow>().FirstOrDefault(x => x.Id.Equals(id));
			return workflow;
		}

		/// <summary>
		/// Gets a workflow from Azure storage by id
		/// </summary>
		/// <param name="id">The id of the workflow to retrieve</param>
		/// <returns>A workflow object</returns>
		public async Task<Workflow> GetAsync(string id)
		{
			var result = workflows.FirstOrDefault(x => x.Id.Equals(id));
			return await Task.FromResult(result);
		}

		/// <summary>
		/// Adds a workflow configuration to blob container asynchronously
		/// </summary>
		/// <param name="item">The workflow item to add to storage</param>
		/// <returns>The added workflow</returns>
		public async Task<Workflow> AddAsync(Workflow item)
		{
			item.Id = item.Name.Replace(" ", string.Empty);
			return UploadBlobToStorage(item);
		}

		/// <summary>
		/// Adds a workflow configuration to blob container
		/// </summary>
		/// <param name="item">The workflow item to add to storage</param>
		/// <returns>The added workflow</returns>
		public Workflow Add(Workflow item)
		{
			item.Id = item.Name.Replace(" ", string.Empty);
			return UploadBlobToStorage(item);
		}

		private Workflow UploadBlobToStorage(Workflow item)
		{
			if (BlobExistsOnCloud(item.Id))
			{
				var workflow = workflows.Cast<Workflow>().FirstOrDefault(x => x.Id.Equals(item.Id));
				WorkflowController.PreExistingWorkflow = workflow;
				return null;
			}

			var blob = cloudBlobContainer.GetBlockBlobReference(item.Id);
			var json = JsonConvert.SerializeObject(item);
			using (var ms = new MemoryStream())
			{
				using (var writer = new StreamWriter(ms))
				{
					writer.Write(json);
					writer.Flush();
					ms.Position = 0;
					blob.UploadFromStreamAsync(ms).Wait();
					if (blob.ExistsAsync().Result)
					{
						workflows.Add(item);
						return item;
					}
				}
			}

			return null;
		}

		private bool BlobExistsOnCloud(string key)
		{
			return cloudBlobContainer.GetBlockBlobReference(key).Exists();
		}

		/// <summary>
		/// Updates a workflow configuration
		/// </summary>
		/// <param name="item">The workflow to update</param>
		/// <returns>True if operation was successful, otherwise false</returns>
		public async Task<Workflow> UpdateAsync(Workflow item)
		{
			var result = UploadBlobToStorage(item);
			return await Task.FromResult(result);
		}

		/// <summary>
		/// Updates a workflow configuration
		/// </summary>
		/// <param name="item">The workflow to update</param>
		/// <returns>True if operation was successful, otherwise false</returns>
		public Workflow Update(Workflow item)
		{
			return UploadBlobToStorage(item);
		}

		/// <summary>
		/// Deletes a workflow from the collection
		/// </summary>
		/// <param name="id">The id of the workflow to remove</param>
		/// <returns>True if operation was successful, otherwise false.</returns>
		public async Task<bool> DeleteAsync(string id)
		{
			var blob = cloudBlobContainer.GetBlockBlobReference(id);
			return await blob.DeleteIfExistsAsync();
		}

		/// <summary>
		/// Deletes a workflow from the collection
		/// </summary>
		/// <param name="id">The id of the workflow to remove</param>
		/// <returns>True if operation was successful, otherwise false.</returns>
		public bool Delete(string id)
		{
			var blob = cloudBlobContainer.GetBlockBlobReference(id);
			return blob.DeleteIfExists();
		}

		/// <summary>
		/// Returns all workflow dtos
		/// </summary>
		/// <returns>All workflows from storage</returns>
		public async Task<List<WorkflowsGet>> ListAllAsync()
		{
			var result = CovertWorkflowsToGetAll();
			return await Task.FromResult(result);
		}

		/// <summary>
		/// Returns all workflow dtos
		/// </summary>
		/// <returns>All workflows from storage</returns>
		public List<WorkflowsGet> ListAll()
		{
			return CovertWorkflowsToGetAll();
		}

		private List<WorkflowsGet> CovertWorkflowsToGetAll()
		{
			var dtoList = new List<WorkflowsGet>();
			foreach (Workflow w in workflows)
			{
				dtoList.Add(new WorkflowsGet(w.Id, w.Name));
			}

			return dtoList;
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