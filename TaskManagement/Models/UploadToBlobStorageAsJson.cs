namespace API.Models
{
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Newtonsoft.Json;

    /// <summary>
    /// Uploads model objects to Azure Blob Storage as JSON documents
    /// </summary>
    public class UploadToBlobStorageAsJson : IModelCommand<CloudStorageAccount>
    {
        private const string CONTENTTYPE = "application/json";

        private readonly object obj;
        private readonly string containerPath;
        private readonly string blobAddressUri;
        private readonly Dictionary<string, string> metadata;

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadToBlobStorageAsJson"/> class.
        /// </summary>
        /// <param name="obj">The model instance to upload</param>
        /// <param name="containerPath">The name of the storage container</param>
        /// <param name="blobAddressUri">The blob storage URI</param>
        /// <param name="metadata">Any additional attributes to associate with the file upload</param>
        public UploadToBlobStorageAsJson(object obj, string containerPath, string blobAddressUri, Dictionary<string, string> metadata)
        {
            this.obj = obj;
            this.containerPath = containerPath;
            this.blobAddressUri = blobAddressUri;
            this.metadata = metadata;
        }

        /// <summary>
        /// Applies class methods to perform object upload.
        /// </summary>
        /// <param name="model">The cloud storage account where the blob will be uploaded</param>
        public void Apply(CloudStorageAccount model)
        {
            var client = model.CreateCloudBlobClient();

            var container = client.GetContainerReference(containerPath);

            if (!container.Exists())
            {
                container.Create();
            }

            var blobReference = container.GetBlockBlobReference(blobAddressUri);

            var blockBlob = blobReference;
            UploadToContainer(blockBlob);
        }

        /// <summary>
        /// Uploads a cloud block blob to a storage container
        /// </summary>
        /// <param name="blobReference">The document block</param>
        private void UploadToContainer(CloudBlockBlob blobReference)
        {
            SetBlobProperties(blobReference);

            using (var ms = new MemoryStream())
            {
                LoadStreamWithJson(ms);
                blobReference.UploadFromStream(ms);
            }
        }

        /// <summary>
        /// Uses the CONTENTTYPE property to indicate the meta data value for storage and applies any meta data to the cloud block before upload.
        /// </summary>
        /// <param name="blobReference">The document clock reference</param>
        private void SetBlobProperties(CloudBlockBlob blobReference)
        {
            blobReference.Properties.ContentType = CONTENTTYPE;
            foreach (var meta in metadata)
            {
                blobReference.Metadata.Add(meta.Key, meta.Value);
            }
        }

        /// <summary>
        /// Serializes a model instance as JSON and writes it to a stream
        /// </summary>
        /// <param name="ms">The object stream</param>
        private void LoadStreamWithJson(Stream ms)
        {
            var json = JsonConvert.SerializeObject(obj);
            using (StreamWriter writer = new StreamWriter(ms))
            {
                writer.Write(json);
                writer.Flush();
                ms.Position = 0;
            }
        }
    }
}