
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaskManagement.App_Start;//this holds the mongocontext file to connect to db
using API.Models;

namespace TaskManagement.DAL
{
    public class UserRepositoryMongo : IUserRepositoryMongo
    {
        private readonly MongoContext _context;
       
        // Injects MongoContext for DI
        public UserRepositoryMongo(MongoContext context)
        {
            _context = context;
        }

        // Catches exception if there are errors with Models inside Context. 
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

        //posts new user object 
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

        public void DeleteUser(int UserID)
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

        public User InsertEmployeeToManager(string managerId, Employee employee)
        {
            throw new NotImplementedException();
        }
    }//end class
}//end namespace