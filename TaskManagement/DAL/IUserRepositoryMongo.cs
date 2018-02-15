using System;
using System.Collections.Generic;
using API.Models;

namespace TaskManagement.DAL {
    
    // Interface for UserRespository that will be used for DI
    public interface IUserRepositoryMongo : IDisposable {

        IEnumerable<User> GetUsers();
         User GetUserByID(string userId);
        // insert employee to manager
        User InsertEmployeeToManager(string managerId, Employee employee);
        //a method that posts user
        System.Threading.Tasks.Task InsertUser(User User);
        void DeleteUser(int UserID);
        void UpdateUser(User User);
        void Save();

    }
}