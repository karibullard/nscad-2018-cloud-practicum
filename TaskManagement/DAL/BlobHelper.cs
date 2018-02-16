namespace API.DAL
{
    using Microsoft.Azure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    /// <summary>
    /// Static helper methods for accessing blob storage
    /// </summary>
    public static class BlobHelper
    {
        public static CloudBlobContainer GetBlobContainer()
        {
            var blobStorageConnectionString = GetConnectionString();
            var blobStorageContainerName = GetContainerName();
            var blobStorageAccount = CloudStorageAccount.Parse(blobStorageConnectionString);
            var blobClient = blobStorageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference(blobStorageContainerName);
        }

        private static string GetConnectionString() => CloudConfigurationManager.GetSetting("BlobStorageConnectionString");

        private static string GetContainerName() => CloudConfigurationManager.GetSetting("BlobStorageContainerName");

        public static string GetBlobName(int blobId)
        {
            switch(blobId)
            {
                case 1:
                    return "CloudOffEx.json";
                case 2:
                    return "CloudOffIn.json";
                case 3:
                    return "CloudOnEx.json";
                case 4:
                    return "CloudOnIn.json";
                case 5:
                    return "DCDOOffEx.json";
                case 6:
                    return "DCDOOffIn.json";
                case 7:
                    return "DCDOOnEx.json";
                case 8:
                    return "DCDOOnIn.json";
                case 9:
                    return "Dev9OffEx.json";
                case 10:
                    return "Dev9OffIn.json";
                case 11:
                    return "Dev9OnEx.json";
                case 12:
                    return "Dev9OnIn.json";
                case 13:
                    return "Dev9OffEx.json";
                case 14:
                    return "Dev9OffIn.json";
                case 15:
                    return "Dev9OnEx.json";
                case 16:
                    return "Dev9OnIn.json";
                case 17:
                    return "Logic2020OffEx.json";
                case 18:
                    return "Logic2020OffIn.json";
                case 19:
                    return "Logic2020OnEx.json";
                case 20:
                    return "Logic2020OnIn.json";
                case 21:
                    return "SharePointOffEx.json";
                case 22:
                    return "SharePointOffIn.json";
                case 23:
                    return "SharePointOnEx.json";
                case 24:
                    return "SharePointOnIn.json";
                default:
                    return null;
            }
        }

    }
}