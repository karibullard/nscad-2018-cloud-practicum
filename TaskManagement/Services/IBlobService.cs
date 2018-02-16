namespace API.Services
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Models;

    public interface IBlobService
    {
        Task<List<BlobUpload>> UploadBlobsAsync(HttpContent httpContent);

        Task<BlobDownload> DownloadBlob(int blobId);
    }
}