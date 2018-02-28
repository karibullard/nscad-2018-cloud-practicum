using API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TaskManagement.DAL;

namespace API.Controllers
{
    /// <summary>
    /// Controller for the User Model.
    /// </summary>
    // [Produces("application/json")]
    [RoutePrefix("api/user")]
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
        /// GET/api/users
        /// </summary>
        /// <returns>A List of Users</returns>
        [HttpGet]
        [Route("")]
        public IEnumerable<User> Get()
        {
            List<User> userList = _userRepository.GetUsers().ToList();

            return userList;
        }

        /// <summary>
        /// GET/api/users/{activeDirectoryId}
        /// Get route for Users. Will return all users in db
        /// </summary>
        /// <param name="activeDirectoryId">The activeDirectoryId that the api will look for</param>
        /// <returns>A User Object</returns>
        [HttpGet]
        [Route("{activeDirectoryId}")]
        public User Get(string activeDirectoryId)
        {
            User user = _userRepository.GetUserByID(activeDirectoryId);
            return user;
        }

        /// <summary>
        /// POST/api/users
        /// Post route to post a new user.
        /// </summary>
        /// <param name="user">A user Object to be posted</param>
        [HttpPost]
        [Route("")]
        public void Post([FromBody]User user)
        {
            _userRepository.InsertUser(user);
        }

        /// <summary>
        /// PUT/api/users/{activeDirectoryId}
        /// Put route that will replace a JSON document with another document filtered by activeDirectoryId.
        /// </summary>
        /// <param name="activeDirectoryId">The user Id that will be used to find the document.</param>
        /// <param name="user">The new User document</param>
        [HttpPut]
        [Route("{activeDirectoryId}")]
        public void Put(string activeDirectoryId, User user)
        {
            _userRepository.UpdateUser(activeDirectoryId, user);
        }

        /// <summary>
        /// Delete route for users
        /// </summary>
        /// <param name="id">id of user to be deleted</param>
        public void Delete(int id)
        {
        }
    }
}
