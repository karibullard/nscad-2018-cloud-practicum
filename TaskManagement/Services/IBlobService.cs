using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using API.DTO;

namespace API.Services
{
	internal interface IBlobService
	{
		Task<List<WorkflowUpload>> UploadBlobs(HttpContent httpContent);

		Task<WorkflowDownload> DownloadBlob(int blobId);
	}
}