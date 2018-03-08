using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

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

		/// <summary>
		/// Initializes a new instance of the <see cref="StorageContext"/> class.
		/// </summary>
		public StorageContext()
			: this(CloudConfigurationManager.GetSetting("StorageConnectionString"))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StorageContext"/> class. Overloaded constructor.
		/// </summary>
		/// <param name="connectionString">The storage account connection string.</param>
		public StorageContext(string connectionString)
		{
			storageAccount = CloudStorageAccount.Parse(connectionString);
			blobClient = storageAccount.CreateCloudBlobClient();
		}

		/// <summary>
		/// Gets Workflow Blob Containter
		/// </summary>
		/// <returns>A workflow blob container reference</returns>
		public CloudBlobContainer GetBlobContainer()
		{
			return blobClient.GetContainerReference(CloudConfigurationManager.GetSetting("StorageContainerName"));
		}
	}
}