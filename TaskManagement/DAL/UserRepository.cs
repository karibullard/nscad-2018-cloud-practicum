
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using TaskManagement.App_Start;//this holds the mongocontext file to connect to db
using API.Models;
using System.Threading.Tasks;


namespace TaskManagement.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoContext _context;


        public UserRepository(MongoContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            try


            {
                List<User> userList = _context.Users.Find(_ => true).ToList();
                return userList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User GetUserByID(string userId)
        {
            var filter = Builders<User>.Filter.Eq("_id", userId);

            try
            {
                return _context.Users
                    .Find(filter)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.Threading.Tasks.Task InsertUser(User user)
        {
            try
            {
                System.Threading.Tasks.Task insertedUser = _context.Users.InsertOneAsync(user);
                return insertedUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteUser(string UserID)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User User)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }//end class
}//end namespace