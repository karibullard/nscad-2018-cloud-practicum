namespace API.Controllers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using DAL;
    using Models;
    using SwaggerTestRequests;
    using Swashbuckle.Examples;
    using Swashbuckle.Swagger.Annotations;

    /// <inheritdoc />
    /// <summary>
    /// Workflow API Endpoints
    /// </summary>
    /// <remarks>Supported Request Types: **POST, *GET, **PUT, and **DELETE
    /// <para>(*authorized users)</para>
    /// <para>(**admin users)</para>
    /// </remarks>
    [RoutePrefix("workflows")]
    public class WorkflowController : ApiController
    {
        private readonly IWorkflowRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkflowController"/> class.
        /// The constructor
        /// </summary>
        /// <param name="repository">The workflow repository</param>
        public WorkflowController(IWorkflowRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// The post controller
        /// </summary>
        /// <param name="workflow">The workflow JSON object to create</param>
        /// <returns>An HTTP response message</returns>
        [SwaggerResponse(HttpStatusCode.Created, "Resource created", typeof(Workflow))]
        [SwaggerRequestExample(typeof(Workflow), typeof(WorkflowPost))]
        [Route("")]
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] Workflow workflow)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "JSON object is invalid.");
                }

                var result = await BlobStorageUtil.UploadAsJson(workflow);
                if (result == null)
                {

                }

                repository.Add(workflow);
                var response = Request.CreateResponse(HttpStatusCode.Created, result);
                response.Content = new StringContent("Success! Workflow has been created.", Encoding.Unicode);
                return response;
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// Get a workflow configuration by id
        /// </summary>
        /// <remarks>
        /// Sample Request id: CloudOffshoreExternal
        /// </remarks>
        /// <param name="id">The workflow id</param>
        /// <returns>"A configuration document for a UST on-boarding workflow."</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Success! UST workflow has been found", typeof(Workflow))]
        [Route("{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(string id)
        {
            var container = BlobStorageUtil.GetWorkflowBlobContainer();
            var blob = container.GetBlockBlobReference(id);
            var cloudBlobExists = await blob.ExistsAsync();
            if (!cloudBlobExists)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Resource not found.");
            }

            Stream blobStream = await blob.OpenReadAsync();
            blobStream.Position = 0;

            var message = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(blobStream),
                RequestMessage = Request,
                ReasonPhrase = "Success! UST workflow has been found."
            };
            message.Headers.Location = new Uri(
                new Uri(Request.RequestUri.AbsoluteUri.Replace(
                    Request.RequestUri.PathAndQuery, string.Empty)), VirtualPathUtility.ToAbsolute($"~/workflow/{id}"));
            message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return message;
        }

        /// <summary>
        /// Update a workflow by id
        /// </summary>
        /// <returns>Key value pairs with workflow names and ids</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Success! UST workflow has been updated", typeof(Workflow))]
        [Route("{id}")]
        [HttpPut]
        public async Task<HttpResponseMessage> Put([FromUri] string id, [FromBody] Workflow requestWorkflow)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, requestWorkflow);
                }

                var workflow = repository.GetAsync(id);
                if (workflow == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Resource not found.");
                }

                var result = await BlobStorageUtil.UploadAsJson(requestWorkflow);
                if (result != null)
                {
                    repository.Remove(workflow.Result);
                    repository.Add(requestWorkflow);
                    var response = Request.CreateResponse(HttpStatusCode.Created, requestWorkflow);
                    response.Headers.Location = new Uri(
                        new Uri(Request.RequestUri.AbsoluteUri.Replace(
                            Request.RequestUri.PathAndQuery, string.Empty)), VirtualPathUtility.ToAbsolute($"~/workflow/{id}"));
                    return response;
                }
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            return null;
        }

        /// <summary>
        /// Delete a workflow by id
        /// </summary>
        /// <returns>Key value pairs with workflow names and ids</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Success! UST workflow has been deleted", typeof(Workflow))]
        [Route("{id}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> Delete([FromUri] string id)
        {
            try
            {
                var workflow = repository.GetAsync(id);
                if (workflow == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Resource not found.");
                }

                return Request.CreateResponse(HttpStatusCode.OK, workflow);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// Get all workflows
        /// </summary>
        /// <returns>Key value pairs with workflow names and ids</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Success! UST workflows have been found", typeof(Workflow))]
        [Route("")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAll()
        {
            var workflows = repository.GetAll();
            if (workflows == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Resource not found.");
            }

            var response = Request.CreateResponse(HttpStatusCode.OK, workflows);
            response.Headers.Location = new Uri(
                new Uri(Request.RequestUri.AbsoluteUri.Replace(
                    Request.RequestUri.PathAndQuery, string.Empty)), VirtualPathUtility.ToAbsolute($"~/workflows"));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return response;
        }
    }
}
