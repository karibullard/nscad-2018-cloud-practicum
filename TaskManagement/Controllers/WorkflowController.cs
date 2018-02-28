using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using API.DAL;
using API.Models;
using API.SwaggerTestRequests;
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
	public class WorkflowController : ApiController
	{
		private readonly IWorkflowRepository repository;

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkflowController"/> class. The constructor
		/// </summary>
		/// <param name="repository">The workflow repository</param>
		public WorkflowController(IWorkflowRepository repository)
		{
			this.repository = repository;
		}

		/// <summary>
		/// Get all workflows
		/// </summary>
		/// <returns>Key value pairs with workflow names and ids</returns>
		[SwaggerResponse(HttpStatusCode.OK, "Success! UST workflows have been found", typeof(Workflow))]
		[Route("")]
		[HttpGet]
		public async Task<HttpResponseMessage> ListAll()
		{
			try
			{
				var result = await repository.ListAll();
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
				return Request.CreateResponse(HttpStatusCode.InternalServerError, "Internal server error." + e.ToString());
			}
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

				var success = await repository.Add(workflow);
				if (!success)
				{
					return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Resource not found.");
				}

				var response = Request.CreateResponse(HttpStatusCode.Created, workflow);
				response.Content = new StringContent("Success! Workflow has been created.", Encoding.Unicode);
				return response;
			}
			catch (Exception e)
			{
				return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
			}
		}

		/// <summary>
		/// Get workflow by id controller
		/// </summary>
		/// <remarks>Sample Request id: CloudOffshoreExternal</remarks>
		/// <param name="id">The workflow id</param>
		/// <returns>"A configuration document for a UST on-boarding workflow."</returns>
		[SwaggerResponse(HttpStatusCode.OK, "Success! UST workflow has been found", typeof(Workflow))]
		[Route("{id}")]
		[HttpGet]
		public async Task<HttpResponseMessage> Get(string id)
		{
			try
			{
				if (id == null)
				{
					return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Id is invalid.");
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
				return Request.CreateResponse(HttpStatusCode.InternalServerError, "Internal server error." + e.ToString());
			}
		}

		/// <summary>
		/// Update a workflow by id
		/// </summary>
		/// <param name="id">The workflow id</param>
		/// <param name="requestWorkflow">The workflow to update.</param>
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

				var success = await repository.Update(requestWorkflow);
				if (!success)
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
				return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
			}
		}

		/// <summary>
		/// Delete a workflow by id
		/// </summary>
		/// <param name="id">The id of the workflow to remove.</param>
		/// <returns>Key value pairs with workflow names and ids</returns>
		[SwaggerResponse(HttpStatusCode.OK, "Success! UST workflow has been deleted", typeof(Workflow))]
		[Route("{id}")]
		[HttpDelete]
		public async Task<HttpResponseMessage> Delete([FromUri] string id)
		{
			try
			{
				var success = await repository.Delete(id);
				if (!success)
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Id.");
				}

				return Request.CreateResponse(HttpStatusCode.OK, "Success! Workflow with id: " + id + " has been deleted");
			}
			catch (Exception e)
			{
				return Request.CreateResponse(HttpStatusCode.InternalServerError, e);
			}
		}
	}
}