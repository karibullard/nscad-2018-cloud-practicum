using System;
using System.Collections.Generic;
using API.Models;

namespace TaskManagement.DAL {
    public interface IUserRepository : IDisposable {

        IEnumerable<User> GetUsers();
        User GetUserByID(int UserId);
        void InsertUser(User User);
        void DeleteUser(int UserID);
        void UpdateUser(User User);
        void Save();

    }
}