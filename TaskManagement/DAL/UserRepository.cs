
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaskManagement.App_Start;//this holds the mongocontext file to connect to db
using API.Models;


namespace TaskManagement.DAL
{
    /// <summary>
    /// A class that extends IUser that will have all methods to access and retrieve data from DB
    /// </summary>
    public class UserRepository : IUserRepository
    {
        // Initialize context we will be using
        private readonly MongoContext _context;

        // Inject context using DI
        public UserRepository(MongoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets List of all Users inside Collection
        /// </summary>
        /// <returns>A list of Users</returns>
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

        /// <summary>
        /// Gets a user based on userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A User matching userId</returns>
        [HttpGet]
        public User GetUserByID(string userId)
        {
            var queryId = userId;

            try
            {
                var entity = _context.Users.Find(
                    i => i.Id == MongoDB.Bson.ObjectId.Parse(userId)).ToList();
                return entity.First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Post a user to DB
        /// </summary>
        /// <param name="user"></param>
        /// <returns>The inserted User</returns>
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

        public void DeleteUser(int UserID)
        {
            throw new NotImplementedException();
        }

        public User InsertEmployeeToManager(string managerId, Employee employee)
        {
            throw new NotImplementedException();
        }
    }//end class
}//end namespace