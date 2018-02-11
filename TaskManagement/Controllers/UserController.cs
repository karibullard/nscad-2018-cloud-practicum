using API.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;//this conflicts with system.web.mvc
//using System.Web.Mvc;
using TaskManagement.DAL;

namespace API.Controllers
{
    public class UserController : ApiController
    {

        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/users
        [HttpGet]
        //public IEnumerable<string> Get() //this was karis origrinal method header 
        public IEnumerable<User> Get()
        {
            //return new string[] { "value1", "value2" };
            return _userRepository.GetUsers().ToList();
        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        public void Post([FromBody]string value)
        {
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
