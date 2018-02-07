using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using MongoDB.Bson;

namespace TaskManagement.DAL {
    public class StorageAccess {

        /// <summary>
        /// Retrieves a reference to the container within Azure storage account which contains the workflows.
        /// </summary>
        internal static CloudBlobContainer GetWorkflowContainer() {
            //Retrieves a connection string from configuration file and returns a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the service client to retrieve containers and blobs stored in Blob storage.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container from configuration file.
            return blobClient.GetContainerReference(CloudConfigurationManager.GetSetting("WorkflowStorageContainerName"));
        }

        /// <summary>
        /// Retrieves a list of workflows from the workflow Azure storage container.
        /// </summary>
        internal static List<string> GetWorkflowNames() {
            var container = GetWorkflowContainer();
            var blobs = container.ListBlobs(useFlatBlobListing: true);
            var listOfFileNames = new List<string>();

            foreach(var blob in blobs) {
                var blobFileName = blob.Uri.Segments.Last();
                listOfFileNames.Add(blobFileName);
            }
            return listOfFileNames;
        }

        /// <summary>
        /// Download workflow from Azure storage using DownloadToStream method.
        /// </summary>
        internal static string GetWorkflow(ObjectId id) {
            // Retrieve a reference to a container.
            var container = GetWorkflowContainer();
            // Retrieve reference to a blob with name "id"
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(id.ToString());

            // Download blob using DownloadToStream method.
            string text;
            using(var memoryStream = new MemoryStream()) {
                blockBlob.DownloadToStream(memoryStream);
                text = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            return text;
        }
    }
}