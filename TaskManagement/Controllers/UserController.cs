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

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
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
