using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using API.DTO;
using API.Models;
using API.DAL;
using MongoDB.Driver; 

namespace TaskManagement.DAL
{
    /// <summary>
    /// Repository for User model that contains business logic.
    /// </summary>
    // TODO: Add Logging.
    public class UserRepositoryMongo : IUserRepositoryMongo
    {
        private readonly MongoContext _context;
        private IList<UserGet> userDTO;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepositoryMongo"/> class.
        /// constructor that will receive injected context
        /// </summary>
        /// <param name="context">the context to be injected</param>
        public UserRepositoryMongo(MongoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>A list of UserDTO</returns>
        [HttpGet]
        public async Task<IList<UserGet>> GetAllAsync()
        {
            userDTO = new List<UserGet>();
            var users = _context.Users;

            var usersList = await users.Find(_ => true).ToListAsync();

            foreach (User element in usersList)
            {
                userDTO.Add(
                new UserGet(element.FirstName, element.LastName, element.ActiveDirectoryId));
            }

            return userDTO;
        }

        /// <summary>
        /// Gets a user based on activeDirectoryId.
        /// </summary>
        /// <param name="activeDirectoryId">the activeDirectoryId the api will use to get user</param>
        /// <returns>A User matching userId</returns>
        [HttpGet]
        public User GetUserById(string activeDirectoryId)
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
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Error 404 No User with ID = {0}", activeDirectoryId))
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
            catch (Exception)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error 500 Invalid Internal Server Error")),
                };
                throw new HttpResponseException(resp);
            }
        }

        /// <summary>
        /// Async operation to get a user based on ActiveDirectory Id.
        /// </summary>
        /// <param name="activeDirectoryId">ActiveDirectoryId</param>
        /// <returns>a User</returns>
        public async Task<User> GetUserByIdAsync(string activeDirectoryId)
        {
            var user = _context.Users.Find(x => x.ActiveDirectoryId.Equals(activeDirectoryId)).ToList();
            return await Task.FromResult(user.First());
        }

        /// <summary>
        /// Creates a new user.
        /// Checks to see if User already exist in DB by using AAD ID
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<User> InsertUser(User user)
        {
            try
            {
                if (user == null)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent(string.Format("Error 400 Bad Request")),
                    };
                    throw new HttpResponseException(resp);
                }

                var duplicateUser = _context.Users.AsQueryable().Where(i => i.ActiveDirectoryId.Equals(user.ActiveDirectoryId)).ToList();
                if (duplicateUser.Count != 0)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.Conflict)
                    {
                        Content = new StringContent(string.Format("User Already Exist")),
                    };
                    throw new HttpResponseException(resp);
                }

                var activeDirectoryId = user.ActiveDirectoryId;
                _context.Users.InsertOne(user);

                var newUser = _context.Users.AsQueryable().Where(i => i.ActiveDirectoryId.Equals(activeDirectoryId));

                return await Task.FromResult(newUser.First());
            }
            catch (HttpResponseException e)
            {
                throw new HttpResponseException(e.Response);

            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="userId">id of User to be deleted</param>
		public void DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update user filtered by UserId and replace the document with a new document.
        /// </summary>
        /// <param name="activeDirectoryId">Used to find the specific User document</param>
        /// <param name="user">The User Object that will replace the current document</param>
        // TODO: Figure out a way to update by only using one query.
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
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("Error 404 No User with ID = {0}", activeDirectoryId))
                };
                throw new HttpResponseException(resp);
            }
            catch (Exception)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(string.Format("Error 500 Invalid Internal Server Error"))
                };
                throw new HttpResponseException(resp);
            }
        }

        /// <summary>
        /// Update a user in CosmosDB
        /// </summary>
        /// <param name="userId">ActiveDirectoryID of user</param>
        /// <param name="user">The User object</param>
        /// <returns>A new updated User object</returns>
        public async Task<User> UpdateUserAsync(string userId, User user)
        {
            var tempUser = _context.Users.Find(i => i.ActiveDirectoryId.Equals(userId));
            user.Id = tempUser.First().Id;

            var filter = Builders<User>.Filter.Where(
                i => i.ActiveDirectoryId.Equals(userId));

            await _context.Users.ReplaceOneAsync(
                filter,
                user);
            var updatedUser = _context.Users.Find(x => x.ActiveDirectoryId == userId);

            return await Task.FromResult(updatedUser.First());
        }

        /// <summary>
        /// not implemented
        /// </summary>
		public void Save()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// not implemented
        /// </summary>
		public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        Task<bool> IUserRepositoryMongo.DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
