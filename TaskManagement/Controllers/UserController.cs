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
    
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        private readonly IUserRepositoryMongo _userRepository;

        // Injects user repository using DI
        public UserController(IUserRepositoryMongo userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/users
        // Returns List of users inside collection. 
        [HttpGet]
        [Route("")]
        public IEnumerable<User> Get()
        {
            List<User> userList = _userRepository.GetUsers().ToList();


            return userList;
        }

        // <summary>
        /// Gets a user based on activeDirectoryId
        /// </summary>
        /// <param name="activeDirectoryId"></param>
        /// <returns>A user based on userId</returns>
        [HttpGet]
        [Route("{activeDirectoryId}")]
        public User Get(string activeDirectoryId)
        {
            User user = _userRepository.GetUserByID(activeDirectoryId);
            return user;
        }

        // POST: api/User
        [HttpPost]
        [Route("")]
        public void Post([FromBody]User user)
        {
            _userRepository.InsertUser(user);
        }

        /// <summary>
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

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
