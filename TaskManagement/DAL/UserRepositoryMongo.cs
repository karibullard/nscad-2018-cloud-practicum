
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaskManagement.App_Start;//this holds the mongocontext file to connect to db
using API.Models;
using MongoDB.Bson;

namespace TaskManagement.DAL
{   
    /// <summary>
    /// Repository for User model that contains business logic. 
    /// </summary>
    public class UserRepositoryMongo : IUserRepositoryMongo
    {
        private readonly MongoContext _context;
       
        // Injects MongoContext for DI
        public UserRepositoryMongo(MongoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all users in Database and return as an IEnumerable<User>.
        /// </summary>
        /// <returns>IEnumerable List of User</returns>
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
        /// Gets a user based on userId.
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
        /// Post a new user object to database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns task.</returns>
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

        public void DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update user filtered by UserId and replace the document with a new document.
        /// </summary>
        /// <param name="userId">Used to find the specific User document</param>
        /// <param name="user">The User Object that will replace the current document</param>
        public void UpdateUser(string userId, User user)
        {
            try
            {
                user.Id = ObjectId.Parse(userId);
                var filter = Builders<User>.Filter.Eq(i => i.Id, user.Id);
                _context.Users.ReplaceOneAsync(
                    filter, 
                    user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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