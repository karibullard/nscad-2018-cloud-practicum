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
        /// Gets a user based on userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A user based on userId</returns>
        [HttpGet]
        [Route("{userID}")]
        public User Get(string userId)
        {
            User user = _userRepository.GetUserByID(userId);
            return user;
        }

        // POST: api/User
        [HttpPost] 
        public void Post([FromBody]User user)
        {
            _userRepository.InsertUser(user);
        }

        /// <summary>
        /// Put route that will replace a JSON document with another document filtered by userId.
        /// </summary>
        /// <param name="userId">The user Id that will be used to find the document.</param>
        /// <param name="user">The new User document</param>
        [HttpPut]
        [Route("{userID}")]
        public void Put(string userId, [FromBody]User user)
        {
            _userRepository.UpdateUser(userId, user);

        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
