﻿using System.Diagnostics;
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

        /// <summary>
        /// Applies class methods to perform object upload.
        /// </summary>
        /// <param name="workflow">The workflow to upload as an object</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> representing the asynchronous operation.</returns>
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

                var workflowId = workflow.Name + Guid.NewGuid();
                workflow.Id = workflowId;
                var blobReference = container.GetBlockBlobReference(workflowId);

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
            } catch (Exception e)
            {
            }

            return fullPath;
        }

        public static List<Workflow> GetWorkflowsFromStorage()
        {
            List<Workflow> workflowList = new List<Workflow>();
            try
            {
                var container = GetWorkflowBlobContainer();

                if(!container.Exists())
                {
                    return null;
                }

                var blobs = container.ListBlobs(null, true, BlobListingDetails.All).Cast<CloudBlockBlob>();
                foreach (var blockBlob in blobs)
                {
                    using(var ms = new MemoryStream())
                    {
                        var workflow = new Workflow();
                        container.GetBlockBlobReference(blockBlob.Name).DownloadToStream(ms);
                        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Workflow));
                        ser.WriteObject(ms, workflow);
                        ms.Position = 0;
                        workflow = (Workflow) ser.ReadObject(ms);
                        workflowList.Add(workflow);
                    }
                }
            } catch(Exception e)
            {
            }

            return workflowList;
        }

        private static string GetConnectionString() => CloudConfigurationManager.GetSetting("StorageConnectionString");

        private static string GetContainerName() => CloudConfigurationManager.GetSetting("StorageContainerName");
    }
}