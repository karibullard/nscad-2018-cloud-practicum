using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace API.DAL
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Runtime.Serialization.Json;
	using System.Text;
	using System.Threading.Tasks;
	using Microsoft.Azure;
	using Microsoft.WindowsAzure.Storage;
	using Microsoft.WindowsAzure.Storage.Blob;
	using Models;
	using Newtonsoft.Json;

	/// <summary>
	/// Azure Blob Storage Helper Methods
	/// </summary>
	public class BlobStorageUtil
	{
		private const string CONTENTTYPE = "application/json";

		/// <summary>
		/// Gets Workflow Blob Containter
		/// </summary>
		/// <returns>A workflow blob container reference</returns>
		public static CloudBlobContainer GetWorkflowBlobContainer()
		{
			var blobStorageAccount = CloudStorageAccount.Parse(GetConnectionString());
			var blobClient = blobStorageAccount.CreateCloudBlobClient();
			return blobClient.GetContainerReference(GetContainerName());
		}

		public static CloudBlobClient GetBlobClient()
		{
			var blobStorageAccount = CloudStorageAccount.Parse(GetConnectionString());
			var blobClient = blobStorageAccount.CreateCloudBlobClient();
			return blobClient;
		}

		/// <summary>
		/// Applies class methods to perform object upload.
		/// </summary>
		/// <param name="workflow">The workflow to upload as an object</param>
		/// <returns>
		/// A <see cref="System.Threading.Tasks.Task"/> representing the asynchronous operation.
		/// </returns>
		public static async Task<string> UploadAsJson(Workflow workflow)
		{
			string fullPath = null;
			try
			{
				var container = GetWorkflowBlobContainer();

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

				var uriBuilder = new UriBuilder(blobReference.Uri) { Scheme = "http" };
				fullPath = uriBuilder.ToString();
			}
			catch (Exception e)
			{
			}

			return fullPath;
		}

		public static IEnumerable<WorkflowGetAllDTO> GetWorkflowsFromStorage()
		{
			List<WorkflowGetAllDTO> workflowList = new List<WorkflowGetAllDTO>();
			try
			{
				var container = GetWorkflowBlobContainer();
				container.CreateIfNotExists();

				var blobs = container.ListBlobs(useFlatBlobListing: true);
				foreach (var blob in blobs)
				{
					workflowList.Add(new WorkflowGetAllDTO() { Name = blob.Uri.Segments.Last(), Id = blob.Uri.Segments.Last() });
				}
			}
			catch (Exception e)
			{
			}

			return workflowList;
		}

		private static string GetConnectionString() => CloudConfigurationManager.GetSetting("StorageConnectionString");

		private static string GetContainerName() => CloudConfigurationManager.GetSetting("StorageContainerName");
	}
}