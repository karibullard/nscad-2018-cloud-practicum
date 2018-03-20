using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using API.DAL;
using API.Models;
using Swashbuckle.Examples;
using Swashbuckle.Swagger.Annotations;

namespace API.Controllers
{
	/// <summary>
	/// Workflow API Endpoints
	/// </summary>
	/// <remarks>
	/// Supported Request Types: **POST, *GET, **PUT, and **DELETE
	/// <para>(*authorized users)</para>
	/// <para>(**admin users)</para>
	/// </remarks>
	[RoutePrefix("workflows")]
	// [Authorize]
	public class WorkflowController : ApiController
	{
		private readonly IWorkflowRepository repository;

		public static Workflow PreExistingWorkflow { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkflowController"/> class. The constructor
		/// </summary>
		/// <param name="repository">The workflow repository</param>
		public WorkflowController(IWorkflowRepository repository)
		{
			this.repository = repository;
		}

		/// <summary>
		/// Get all available workflows from storage
		/// </summary>
		/// <returns>Key value pairs with workflow names and ids</returns>
		/// <response code="200">Success! Workflows have been found.</response>
		/// <response code="400">Bad request.</response>
		/// <response code="401">Authorization information is missing or invalid.</response>
		/// <response code="403">Operation not authorized.</response>
		/// <response code="500">Internal server error.</response>
		/// <response code="501">Service not yet implemented.</response>
		[Route("")]
		[HttpGet]
		[SwaggerResponse(HttpStatusCode.OK, "Success! Workflows have been found.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.BadRequest, "Bad request.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.Unauthorized, "Authorization information is missing or invalid.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.Forbidden, "Operation not authorized.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, "Internal server error.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.NotImplemented, "Service not yet implemented.", typeof(Workflow))]
		public async Task<HttpResponseMessage> ListAll()
		{
			try
			{
				var result = await repository.ListAllAsync();
				if (result.Count < 1 || result == null)
				{
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Resource not found.");
				}

				var response = Request.CreateResponse(HttpStatusCode.OK, result);
				response.ReasonPhrase = "Success! Workflows have been found.";
				response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				return response;
			}
			catch (Exception e)
			{
				// TODO Integrate Error Logging
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error. Something went awry.");
			}
		}

		/// <summary>
		/// Creates a new workflow configuration
		/// </summary>
		/// <param name="workflow">Adds a single workflow.</param>
		/// <returns>An HTTP response message</returns>
		/// <response code="201">Success! Workflow has been created.</response>
		/// <response code="400">Bad request.</response>
		/// <response code="401">Authorization information is missing or invalid.</response>
		/// <response code="403">Operation not authorized.</response>
		/// <response code="500">Internal server error.</response>
		/// <response code="501">Service not yet implemented.</response>
		[HttpPost]
		[Route("")]
		[SwaggerRequestExample(typeof(Workflow), typeof(WorkflowPost201RequestExample))]
		[SwaggerResponse(HttpStatusCode.Created, "Success! Workflow has been created.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.BadRequest, "Bad request.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.Unauthorized, "Authorization information is missing or invalid.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.Forbidden, "Operation not authorized.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, "Internal server error.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.NotImplemented, "Service not yet implemented.", typeof(Workflow))]
		public async Task<HttpResponseMessage> Post([FromBody] Workflow workflow)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "JSON object is invalid.");
				}

				var success = await repository.AddAsync(workflow);
				if (success == null)
				{
					if (PreExistingWorkflow == null)
					{
						return Request.CreateErrorResponse(HttpStatusCode.Conflict, "Resource already exists. Use 'workflow/" + workflow.Id + "' endpoint to update workflow.");
					}
					else
					{
						return Request.CreateErrorResponse(HttpStatusCode.Conflict, PreExistingWorkflow.ToString());
					}
				}

				var response = Request.CreateResponse(HttpStatusCode.Created, workflow);
				response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				response.ReasonPhrase = "Success! Workflow has been created.";
				return response;
			}
			catch (Exception e)
			{
				// TODO Integrate Error Logging
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error. Something went awry.");
			}
		}

		/// <summary>
		/// Retrieves a workflow by id.
		/// </summary>
		/// <remarks>Sample Request id: CloudOffshoreExternal</remarks>
		/// <param name="id">The id of workflow to get.</param>
		/// <returns>"A configuration document for a UST on-boarding workflow."</returns>
		/// <response code="200">Success! Workflow has been found.</response>
		/// <response code="400">Bad request.</response>
		/// <response code="401">Authorization information is missing or invalid.</response>
		/// <response code="403">Operation not authorized.</response>
		/// <response code="500">Internal server error.</response>
		/// <response code="501">Service not yet implemented.</response>
		[HttpGet]
		[Route("{id}")]
		// [SwaggerOperation("WorkflowsIdGet")] //not sure if we need this if we dont remove it
		[SwaggerResponse(HttpStatusCode.OK, "Success! UST workflow has been found", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.BadRequest, "Bad request.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.Unauthorized, "Authorization information is missing or invalid.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.Forbidden, "Operation not authorized.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, "Internal server error.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.NotImplemented, "Service not yet implemented.", typeof(Workflow))]
		public async Task<HttpResponseMessage> Get(string id)
		{
			try
			{
				if (id == null)
				{
					return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request.");
				}

				var workflow = await repository.GetAsync(id);
				if (workflow.Equals(null))
				{
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Resource not found.");
				}

				var response = Request.CreateResponse(HttpStatusCode.OK, workflow);
				response.ReasonPhrase = "Success! UST workflow has been found.";
				response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				return response;
			}
			catch (Exception e)
			{
				// TODO Integrate Error Logging
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error. Something went awry.");
			}
		}

		/// <summary>
		/// Updates an existing workflow by id
		/// </summary>
		/// <param name="id">&gt;The id of workflow to amend.</param>
		/// <param name="requestWorkflow">The workflow to update.</param>
		/// <returns>Key value pairs with workflow names and ids</returns>
		/// <response code="201">Success! Workflow has been updated.</response>
		/// <response code="400">Bad request.</response>
		/// <response code="401">Authorization information is missing or invalid.</response>
		/// <response code="403">Operation not authorized.</response>
		/// <response code="500">Internal server error.</response>
		/// <response code="501">Service not yet implemented.</response>
		[HttpPut]
		[Route("{id}")]
		// [SwaggerOperation("WorkflowsIdPut")] //not sure if we need this if we dont remove it
		[SwaggerResponse(HttpStatusCode.OK, "Success! UST workflow has been updated", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.BadRequest, "Bad request.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.Unauthorized, "Authorization information is missing or invalid.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.Forbidden, "Operation not authorized.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, "Internal server error.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.NotImplemented, "Service not yet implemented.", typeof(Workflow))]
		public async Task<HttpResponseMessage> Put([FromUri] string id, [FromBody] Workflow requestWorkflow)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, requestWorkflow);
				}

				var success = await repository.UpdateAsync(requestWorkflow);
				if (success == null)
				{
					return Request.CreateResponse(HttpStatusCode.NotFound, "Resource not found.");
				}

				var response = Request.CreateResponse(HttpStatusCode.Created, requestWorkflow);
				response.ReasonPhrase = "Success! UST workflow has been updated.";
				response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				return response;
			}
			catch (Exception e)
			{
				// TODO Integrate Error Logging
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error. Something went awry.");
			}
		}

		/// <summary>
		/// Deletes an existing workflow by id
		/// </summary>
		/// <param name="id">The id of the workflow to remove.</param>
		/// <returns>Key value pairs with workflow names and ids</returns>
		/// <response code="200">Success! Workflow has been deleted.</response>
		/// <response code="400">Bad request.</response>
		/// <response code="401">Authorization information is missing or invalid.</response>
		/// <response code="403">Operation not authorized.</response>
		/// <response code="500">Internal server error.</response>
		/// <response code="501">Service not yet implemented.</response>
		[HttpDelete]
		[Route("{id}")]
		[SwaggerOperation("WorkflowsIdDelete")]
		[SwaggerResponse(HttpStatusCode.OK, "Success! UST workflow has been deleted", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.BadRequest, "Bad request.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.Unauthorized, "Authorization information is missing or invalid.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.Forbidden, "Operation not authorized.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.InternalServerError, "Internal server error.", typeof(Workflow))]
		[SwaggerResponse(HttpStatusCode.NotImplemented, "Service not yet implemented.", typeof(Workflow))]
		public async Task<HttpResponseMessage> Delete([FromUri] string id)
		{
			try
			{
				var success = await repository.DeleteAsync(id);
				if (!success)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Id.");
				}

				return Request.CreateResponse(HttpStatusCode.OK, "Success! Workflow with id: " + id + " has been deleted");
			}
			catch (Exception e)
			{
				// TODO Integrate Error Logging
				return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Internal server error. Something went awry.");
			}
		}
	}
}