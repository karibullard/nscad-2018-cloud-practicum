namespace API.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using DAL;
    using Models;

    public class BlobService : IBlobService
    {
        public async Task<List<BlobUpload>> UploadBlobsAsync(HttpContent httpContent)
        {
            var blobUploadProvider = new BlobStorageUploadProvider();

            var list = await httpContent.ReadAsMultipartAsync(blobUploadProvider)
                .ContinueWith(task =>
                {
                    if (task.IsFaulted || task.IsCanceled)
                    {
                        if (task.Exception != null)
                        {
                            throw task.Exception;
                        }
                    }

                    var provider = task.Result;
                    return provider.Uploads.ToList();
                });

            return list;
        }

        public async Task<BlobDownload> DownloadBlob(int blobId)
        {
            var blobName = GetBlobName(blobId);

            if (string.IsNullOrEmpty(blobName))
            {
                return null;
            }

            var container = BlobHelper.GetBlobContainer();
            var blob = container.GetBlockBlobReference(blobName);

            var ms = new MemoryStream();
            await blob.DownloadToStreamAsync(ms);

            var lastPos = blob.Name.LastIndexOf('/');
            var fileName = blob.Name.Substring(lastPos + 1, blob.Name.Length - lastPos - 1);

            var download = new BlobDownload
            {
                BlobStream = ms,
                BlobFileName = fileName,
                BlobLength = blob.Properties.Length,
                BlobContentType = blob.Properties.ContentType
            };

            return download;

        }

        private string GetBlobName(int blobId)
        {
            return string.Empty;
        }
    }
}