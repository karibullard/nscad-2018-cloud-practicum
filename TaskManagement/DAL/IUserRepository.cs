using System;
using System.Collections.Generic;
using API.Models;

namespace TaskManagement.DAL {
    
    /// <summary>
    /// Interface that will be used to access data using DI
    /// </summary>
    public interface IUserRepository : IDisposable {
        // get all users inside collection
        IEnumerable<User> GetUsers();
        // get user by id
        User GetUserByID(string UserId);
        //a method that posts user
        System.Threading.Tasks.Task InsertUser(User User);
        // insert employee to manager
        User InsertEmployeeToManager(string managerId, Employee employee);
        // delete a user
        void DeleteUser(int UserID);
        // update a user
        void UpdateUser(User User);
        // save to db 
        void Save();

    }
}