using System;
using System.Collections.Generic;
using API.Models;

namespace TaskManagement.DAL {
    public interface IUserRepository : IDisposable {

        IEnumerable<User> GetUsers();
        User GetUserByID(string UserId);
        System.Threading.Tasks.Task InsertUser(User User);
        void DeleteUser(string UserID);
        void UpdateUser(User User);
        void Save();

    }
}