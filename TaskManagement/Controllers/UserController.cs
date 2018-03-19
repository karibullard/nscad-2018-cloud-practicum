namespace API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web.Http;
    using API.DTO;
    using API.GraphHelper;
    using API.Models;
    using Swashbuckle.Swagger.Annotations;
    using TaskManagement.DAL;
    using static Microsoft.Graph.GraphServiceClient;

    /// <summary>
    /// Controller for the User Model.
    /// </summary>
    // [Produces("application/json")]
    [RoutePrefix("user")]
    // [Authorize] Temporarily Disabled Until Auth is Fully Enabled. 
    public class UserController : ApiController
    {
        private readonly IUserRepositoryMongo _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// Constructor that accepts the injecton of UserRepository
        /// </summary>
        /// <param name="userRepository">The Injected Repository</param>
        public UserController(IUserRepositoryMongo userRepository) => _userRepository = userRepository;

        /// <summary>
        /// GET/api/users - Get all users
        /// </summary>
        /// <returns>A List of Users</returns>
        /// <response code="200">Success! Users have been found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Authorization information is missing or invalid.</response>
        /// <response code="403">Operation not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <response code="501">Service not yet implemented.</response>
        [HttpGet]
        [Authorize] // Temporary For Testing Purposes.
        [Route("")]
        // [SwaggerOperation("UsersGet")]  //not sure if we need this if we dont remove it
        [SwaggerResponse(HttpStatusCode.OK, "Success! Users have been found.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Bad request.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Authorization information is missing or invalid.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.Forbidden, "Operation not authorized.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Internal server error.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.NotImplemented, "Service not yet implemented.", typeof(User))]
        public async Task<HttpResponseMessage> GetAll()
        {
            IList<UserGet> result = new List<UserGet>();
            try
            {
                result = await _userRepository.GetAllAsync();

                if (result.Count < 1 || result == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Resource not found.");
                }

                var response = Request.CreateResponse(HttpStatusCode.OK, result);
                response.ReasonPhrase = "Success! Users have been found.";
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (HttpResponseException e)
            {
                throw new HttpResponseException(e.Response);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Internal server error." + e.ToString());
            }
        }

        /// <summary>
        /// GET/api/users/{activeDirectoryId}
        ///  Retrieve a user by id.
        /// </summary>
        /// <remarks>Returns a user by id. If the user is of type \&quot;employee,\&quot; The user object will contain the
        /// id of the workflow a user has been assigned, as well as an array of task ids representing the tasks a user has completed.
        /// If the user is of type \&quot;manager,\&quot; the object will contain an an associative array where the key is the employee
        /// ID and the value if the employee&#39;s name (first and last) for the employees they manage
        /// </remarks>
        /// <param name="activeDirectoryId">The id of the user to retrieve which is the activeDirectoryId that the api will look for</param>
        /// <returns>A User Object</returns>
        /// <response code="200">Success! User has been found.</response>
        /// <response code="400">Bad request. User not found.</response>
        /// <response code="401">Authorization information is missing or invalid.</response>
        /// <response code="403">Operation not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <response code="501">Service not yet implemented.</response>
        [HttpGet]
        [Route("{activeDirectoryId}")]
        // [SwaggerOperation("UsersIdGet")] //not sure if we need this if we dont remove it
        [SwaggerResponse(HttpStatusCode.OK, "Success! User have been found.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Bad request.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Authorization information is missing or invalid.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.Forbidden, "Operation not authorized.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Internal server error.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.NotImplemented, "Service not yet implemented.", typeof(User))]
        public async Task<HttpResponseMessage> Get(string activeDirectoryId)
        {
            try
            {
                var result = await _userRepository.GetUserByIdAsync(activeDirectoryId);

                if (result == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Resource not found.");
                }

                var response = Request.CreateResponse(HttpStatusCode.OK, result);
                response.ReasonPhrase = "Success! User have been found.";
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                return response;
            }
            catch (HttpResponseException e)
            {
                throw new HttpResponseException(e.Response);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Internal server error." + e.ToString());
            }
        }

        /// <summary>
        /// POST/api/users
        /// Post route to post a new user.
        /// Create a new user
        /// </summary>
        /// <param name="user">A user Object to be posted</param>
        /// <response code="201">Success! User record has been created.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Authorization information is missing or invalid.</response>
        /// <response code="403">Operation not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <response code="501">Service not yet implemented</response>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        [Route("")]
        // [SwaggerOperation("UsersPost")] //not sure if we need this if we dont remove it
        [SwaggerResponse(HttpStatusCode.OK, "Success! User record has been created.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Bad request.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Authorization information is missing or invalid.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.Forbidden, "Operation not authorized.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Internal server error.", typeof(User))] 
        public async Task<HttpResponseMessage> Post([FromBody]User user)
        {
            GraphRepository graphRepository = new GraphRepository();
            if (ModelState.IsValid)
            {
                try
                {
                    // function => try to find or create user.email
                    //      returns active directory id
                    string activeDirectoryId = await graphRepository.FindOrCreateUser(user);

                    // update the user object with the active directory id
                    user.ActiveDirectoryId = activeDirectoryId;


                    var result = await _userRepository.InsertUser(user);
                    if (result == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad request.");
                    }

                    var response = Request.CreateResponse(HttpStatusCode.OK, result);
                    response.ReasonPhrase = "Success! User with AAD ID " + activeDirectoryId + " has been created.";
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return response;
                }
                catch (HttpResponseException e)
                {
                    throw new HttpResponseException(e.Response);
                }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Internal server error." + e.ToString());
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "ModelState is not Valid");
            }
        }

        /// <summary>
        /// PUT/api/users/{activeDirectoryId}
        /// Update an existing user.
        /// Put route that will replace a JSON document with another document filtered by activeDirectoryId.
        /// </summary>
        /// <remarks>Updates the user record associated with the id set in the request.
        ///  The entire User object must be sent in the body of the request. This endpoint is also how employee
        ///  users will indicate task completion, and manager users will be assigned employees. These assignments can
        ///  be made by updating the User object and the appropriate properties, and sending the updated JSON in the body of the request.
        ///  </remarks>
        /// <param name="activeDirectoryId">The user Id that will be used to find the document.</param>
        /// <param name="user">The new User document</param>
        /// <response code="201">Success! User record has been updated.</response>
        /// <response code="400">Bad request. User not found.</response>
        /// <response code="401">Authorization information is missing or invalid.</response>
        /// <response code="403">Operation not authorized.</response>
        /// <response code="500">Internal server error.</response>
        /// <response code="501">Service not yet implemented.</response>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPut]
        [Route("{activeDirectoryId}")]
        // [SwaggerOperation("UsersIdPut")] //not sure if we need this if we dont remove it
        [SwaggerResponse(HttpStatusCode.OK, "Success! User record has been updated.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Bad request.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.Unauthorized, "Authorization information is missing or invalid.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.Forbidden, "Operation not authorized.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Internal server error.", typeof(User))]
        [SwaggerResponse(HttpStatusCode.NotImplemented, "Service not yet implemented.", typeof(User))]
        public async Task<HttpResponseMessage> Put(string activeDirectoryId, User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _userRepository.UpdateUserAsync(activeDirectoryId, user);
                    if (result == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Resource not found.");
                    }

                    var response = Request.CreateResponse(HttpStatusCode.OK, result);
                    response.ReasonPhrase = "Success! User record have been updated.";
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    return response;
                }
                catch (HttpResponseException e)
                {
                    throw new HttpResponseException(e.Response);
                }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Internal server error." + e.ToString());
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "ModelState not valid");
            }
        }

        /// <summary>
        /// Delete route for users
        /// </summary>
        /// <param name="activeDirectoryId">id of user to be deleted</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpDelete]
        [Route("{activeDirectoryId}")]
        public async Task<HttpResponseMessage> Delete(string activeDirectoryId)
        {
            return Request.CreateResponse(HttpStatusCode.NotImplemented, "Function not implemented.");
        }
    }
}
