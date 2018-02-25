namespace TaskManagement.DAL
{
    using System;
    using System.Collections.Generic;
    using API.Models;

    /// <summary>
    /// Interface for User Repository methods. 
    /// </summary>
    public interface IUserRepositoryMongo : IDisposable {

        // Get all users
        IEnumerable<User> GetUsers();

         User GetUserByID(string userId);

        // A method that posts user
        void InsertUser(User User);

        void DeleteUser(int userId);

        // A method that replaces one JSON document with another filtered by userId.       
        void UpdateUser(string userId, User User);

        void Save();

    }
}