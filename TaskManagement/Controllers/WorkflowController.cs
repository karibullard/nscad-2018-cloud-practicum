namespace API.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
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
    [RoutePrefix("workflow")]
    public class WorkflowController: ApiController
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
                    return Request.CreateResponse(HttpStatusCode.BadRequest, workflow);
                }

                var result = await BlobStorageUtil.UploadAsJson(workflow);
                if (result != null)
                {
                    repository.Add(workflow);
                    var response = Request.CreateResponse(HttpStatusCode.Created, result);
                    response.Content = new StringContent("Success! Workflow has been created.", Encoding.Unicode);
                    return response;
                }
            } catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
            }

            return null;
        }

        /// <summary>
        /// Get a workflow configuration by id
        /// </summary>
        /// <param name="id">The workflow id</param>
        /// <returns>"A configuration document for a UST on-boarding workflow."</returns>
        [SwaggerResponse(HttpStatusCode.OK, "Success! UST workflow has been found", typeof(Workflow))]
        [Route("{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(string id)
        {
            var workflow = repository.Get(id);
            if (workflow == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Resource not found.");
            }

            return Request.CreateResponse(HttpStatusCode.OK, workflow);
        }
    }
}
