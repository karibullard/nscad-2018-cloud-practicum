using API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TaskManagement.DAL;

namespace API.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserRepository _userRepository;

        // Injects user repository using DI
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/users
        // Returns List of users inside collection. 
        [HttpGet]
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
