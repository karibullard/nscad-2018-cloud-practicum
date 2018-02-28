using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Responses;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace API.DAL
{
	/// <summary>
	/// Blob Storage Access for Workflow Resource
	/// </summary>
	public class StorageContext
	{
		private const string CONTENTTYPE = "application/json";
		private static CloudStorageAccount storageAccount;
		private static CloudBlobClient blobClient;
		private IList<WorkflowsGet> workflowDtoList;
		private IList<Workflow> workflowList;

		/// <summary>
		/// Initializes a new instance of the <see cref="StorageContext"/> class.
		/// </summary>
		public StorageContext()
			: this(GetConnectionString())
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StorageContext"/> class. Overloaded constructor.
		/// </summary>
		/// <param name="connectionString">The storage account connection string.</param>
		public StorageContext(string connectionString)
		{
			if (storageAccount == null)
			{
				storageAccount = CloudStorageAccount.Parse(connectionString);
			}

			if (blobClient == null)
			{
				blobClient = storageAccount.CreateCloudBlobClient();
			}
		}

		/// <summary>
		/// Gets Workflow Blob Containter
		/// </summary>
		/// <returns>A workflow blob container reference</returns>
		public CloudBlobContainer GetContainer() => blobClient.GetContainerReference(GetContainerName());

		/// <summary>
		/// Gets an enumerable collection of WorkflowDtos from the blob containter.
		/// </summary>
		/// <returns>A workflow blob container reference</returns>
		public async Task<IList<WorkflowsGet>> GetDtoWorkflows()
		{
			if (workflowDtoList == null)
			{
				workflowDtoList = await System.Threading.Tasks.Task.Run(() => GetWorkflowDtos());
			}

			return workflowDtoList;
		}

		private IList<WorkflowsGet> GetWorkflowDtos()
		{
			workflowDtoList = new List<WorkflowsGet>();
			var container = GetContainer();

			var blobs = container.ListBlobs(useFlatBlobListing: true);
			foreach (var blob in blobs)
			{
				workflowDtoList.Add(new WorkflowsGet(blob.Uri.Segments.Last(), blob.Uri.Segments.Last()));
			}

			return workflowDtoList;
		}

		/// <summary>
		/// Gets an enumerable collection of Workflows from the blob containter.
		/// </summary>
		/// <returns>A workflow blob container reference</returns>
		public IList<Workflow> GetWorkflows()
		{
			if (workflowList == null)
			{
				workflowList = GetWorkflowsFromStorage().Result;
			}

			return workflowList;
		}

		private async Task<IList<Workflow>> GetWorkflowsFromStorage()
		{
			workflowList = new List<Workflow>();
			var container = GetContainer();
			var results = container.ListBlobs();
			foreach (IListBlobItem item in results)
			{
				CloudBlob blob = container.GetBlobReference(item.Uri.Segments.Last());
				Stream blobStream = await blob.OpenReadAsync();
				blobStream.Position = 0;
				var rawJson = new StreamReader(blobStream).ReadToEnd();
				var json = JObject.Parse(rawJson);
				var workflow = new Workflow();
				workflow.Id = json["id"].ToString();
				workflow.Name = json["name"].ToString();
				workflow.Description = json["description"].ToString();
				workflow.Tasks = json["tasks"].ToObject<List<Models.Task>>();
				workflowList.Add(workflow);
			}

			return workflowList;
		}

		/// <summary>
		/// Applies class methods to perform object upload.
		/// </summary>
		/// <param name="workflow">The workflow to upload as an object</param>
		/// <returns>
		/// A <see cref="System.Threading.Tasks.Task"/> representing the asynchronous operation.
		/// </returns>
		public async Task<Workflow> UploadAsJson(Workflow workflow)
		{
			try
			{
				var container = GetContainer();

				if (!container.Exists())
				{
					container.Create();
				}

				workflow.Id = workflow.Name;
				var blobReference = container.GetBlockBlobReference(workflow.Id);

				blobReference.Properties.ContentType = CONTENTTYPE;
				var json = JsonConvert.SerializeObject(workflow);
				using (var ms = new MemoryStream())
				{
					using (var writer = new StreamWriter(ms))
					{
						writer.Write(json);
						writer.Flush();
						ms.Position = 0;
						await blobReference.UploadFromStreamAsync(ms);
					}
				}

				return workflow;
			}
			catch (Exception e)
			{
			}

			return null;
		}

		/// <summary>
		/// Deletes a blob from the container by id
		/// </summary>
		/// <param name="id">The id of the workflow to delete</param>
		/// <returns>True if success, otherwise false.</returns>
		public async Task<bool> DeleteAsync(string id)
		{
			var container = GetContainer();
			var blob = container.GetBlobReference(id);
			return await blob.DeleteIfExistsAsync();
		}

		private static string GetConnectionString() => CloudConfigurationManager.GetSetting("StorageConnectionString");

		private static string GetContainerName() => CloudConfigurationManager.GetSetting("StorageContainerName");
	}
}