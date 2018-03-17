using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Models;

namespace TaskManagement.DAL
{
    /// <summary>
    /// Interface for User Repository methods.
    /// </summary>
    public interface IUserRepositoryMongo : IDisposable
    {
        /// <summary>
        /// Returns a list of Users
        /// </summary>
        /// <returns>A list of Users</returns>
        Task<IList<UserGet>> GetAllAsync();

        /// <summary>
        /// Get a user by using activeDirectoryId
        /// </summary>
        /// <param name="activeDirectoryId">AD Id</param>
        /// <returns>A User</returns>
        User GetUserById(string activeDirectoryId);

        /// <summary>
        /// Get User by Id using Async
        /// </summary>
        /// <param name="activeDirectoryId">AD Id of user</param>
        /// <returns>A User</returns>
        Task<User> GetUserByIdAsync(string activeDirectoryId);

        /// <summary>
        /// Post a User
        /// </summary>
        /// <param name="user">User object to be posted</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> representing the asynchronous operation.Bool whether or not update went through</returns>
        Task<User> InsertUser(User user);

        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="userId">Id of User to be deleted</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> representing the asynchronous operation.Bool whether or not update went through</returns>
		Task<bool> DeleteUser(int userId);

        /// <summary>
        /// Update a user by replacing it with a new object.
        /// </summary>
        /// <param name="activeDirectoryId">AD id</param>
        /// <param name="user">A new updated user object</param>
        void UpdateUser(string activeDirectoryId, User user);

        /// <summary>
        /// Put user
        /// </summary>
        /// <param name="userId">Id of user to edit</param>
        /// <param name="user">Edited User object</param>
        /// <returns>A <see cref="System.Threading.Tasks.Task"/> representing the asynchronous operation. Bool wether or not update went through</returns>
        Task<User> UpdateUserAsync(string userId, User user);

        /// <summary>
        /// Save to db
        /// </summary>
		void Save();
    }
}
