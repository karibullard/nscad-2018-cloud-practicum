using API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TaskManagement.DAL;

namespace API.Controllers
{
    /// <summary>
    /// User controller for all User crud Endpoint
    /// </summary>
    public class UserController : ApiController
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Injects repository using DI
        /// </summary>
        /// <param name="userRepository"></param>
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Gets a list of User from collection
        /// </summary>
        /// <returns>A List of Users</returns>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            List<User> userList = _userRepository.GetUsers().ToList();


            return userList;
        }

        /// <summary>
        /// Gets a user based on userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A user based on userId</returns>
        [HttpGet]
        [Route("~/api/user/{userId}/")]
        public User Get(string userId)
        {
            User user = _userRepository.GetUserByID(userId);
            return user;
        }

        // POST: api/User
        public void Post([FromBody]User user)
        {
            _userRepository.InsertUser(user);
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
