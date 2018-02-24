
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagement.App_Start;//this holds the mongocontext file to connect to db
using API.Models;
using System.Net.Http;
using System.Web.Http;
using System.Net;

namespace TaskManagement.DAL
{   
    /// <summary>
    /// Repository for User model that contains business logic. 
    /// </summary>
    //TODO: Add Logging. 

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
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error 500 Invalid Internal Server Error")),
                };
                throw new HttpResponseException(resp);
            }
        }

        /// <summary>
        /// Gets a user based on activeDirectoryId.
        /// </summary>
        /// <param name="activeDirectoryId"></param>
        /// <response code="200">A User object.</response>
        /// <response code="404">Bad request. User not found.</response>
        /// <response code="403">Operation not authorized. Auth is not yet integrated this will be for future reference</response>
        /// <response code="500">Internal server error.</response>
        /// <returns>A User matching userId</returns>
        [HttpGet]
        public User GetUserByID(string activeDirectoryId)
        {
            var queryId = activeDirectoryId;

            try
            {
                var entity = _context.Users.Find(
                    i => i.ActiveDirectoryId == activeDirectoryId).ToList();

                return entity.First();
            }
            catch (System.InvalidOperationException)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Error 404 No User with ID = {0}", activeDirectoryId)),
                };
                throw new HttpResponseException(resp);
            }
            catch (System.Reflection.TargetInvocationException)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Error 400 Invalid ID format")),
                };
                throw new HttpResponseException(resp);
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error 500 Invalid Internal Server Error")),
                };
                throw new HttpResponseException(resp);
            }

        }

        /// <summary>
        /// Post a new user object to database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns task.</returns>
        public void InsertUser(User user)
        {
            try
            {
                  _context.Users.InsertOne(user);
                 
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
        /// <response code="204">No Response Body but Put went through.</response>
        /// <response code="404">Bad request. User not found.</response>
        /// <response code="403">Operation not authorized. Auth is not yet integrated this will be for future reference</response>
        /// <response code="500">Internal server error.</response>
        /// <param name="activeDirectoryId">Used to find the specific User document</param>
        /// <param name="user">The User Object that will replace the current document</param>
        //TODO: Figure out a way to update by only using one query.
        public void UpdateUser(string activeDirectoryId, User user)
        {
            try
            {
                var tempUser = _context.Users.Find(i => i.ActiveDirectoryId.Equals(activeDirectoryId));
                user.Id = tempUser.First().Id;

                var filter = Builders<User>.Filter.Where(
                    i => i.ActiveDirectoryId.Equals(activeDirectoryId));
          
                _context.Users.ReplaceOneAsync(
                    filter,
                    user);
            }
            catch (System.InvalidOperationException)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Error 404 No User with ID = {0}", activeDirectoryId)),
                };
                throw new HttpResponseException(resp);
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error 500 Invalid Internal Server Error")),
                };
                throw new HttpResponseException(resp);
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