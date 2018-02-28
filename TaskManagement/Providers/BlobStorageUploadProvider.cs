using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using API.DAL;
using API.DTO;
using API.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace API.Providers
{
	public class BlobStorageUploadProvider : MultipartStreamProvider
	{
		public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
		{
			Stream stream = null;
			ContentDispositionHeaderValue contentDisposition = headers.ContentDisposition;
			if (contentDisposition != null)
			{
				if (!string.IsNullOrWhiteSpace(contentDisposition.FileName))
				{
					string connectionString = ConfigurationManager.AppSettings["StorageConnectionString"];
					string containerName = ConfigurationManager.AppSettings["StorageContainerName"];
					CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
					CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
					CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);

					CloudBlockBlob blob = blobContainer.GetBlockBlobReference(contentDisposition.FileName);
					stream = blob.OpenWrite();
				}
			}

			return stream;
		}
	}
}