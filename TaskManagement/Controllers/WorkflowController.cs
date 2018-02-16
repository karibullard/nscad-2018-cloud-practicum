using API.DAL;

namespace API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Models;
    using Services;

    /// <summary>
    /// Workflow API Endpoints
    /// </summary>
    /// <remarks>Supported Request Types: **POST, *GET, **PUT, and **DELETE
    /// <para>(*authorized users)</para>
    /// <para>(**admin users)</para>
    /// </remarks>
    public class WorkflowController : ApiController
    {
        private readonly IBlobService service = new BlobService();
        private IWorkflowRepository repository;

        public WorkflowController(IWorkflowRepository repository)
        {
            this.repository = repository;
        }

        [ResponseType(typeof(List<BlobUpload>))]
        public async Task<IHttpActionResult> PostBlobUpload()
        {
            try
            {
                // This endpoint only supports multipart form data
                if (!Request.Content.IsMimeMultipartContent("form-data"))
                {
                    return StatusCode(HttpStatusCode.UnsupportedMediaType);
                }

                var result = await service.UploadBlobsAsync(Request.Content);
                if (result != null && result.Count > 0)
                {
                    return Ok(result);
                }

                // Otherwise
                return BadRequest();

                // Call service to perform upload, then check result to return as content
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
