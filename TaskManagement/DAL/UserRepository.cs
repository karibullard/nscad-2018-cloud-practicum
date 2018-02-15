
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaskManagement.App_Start;//this holds the mongocontext file to connect to db
using API.Models;

namespace TaskManagement.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoContext _context;
       
        // Injects MongoContext for DI
        public UserRepository(MongoContext context)
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

        public User GetUserByID(int UserId)
        {
            var filter = Builders<User>.Filter.Eq("Id", UserId);

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

        public void InsertUser(User User)
        {
            throw new NotImplementedException();
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
    }//end class
}//end namespace