using System;
using System.Collections.Generic;
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
		IEnumerable<User> GetUsers();

        /// <summary>
        /// Get users by Id
        /// </summary>
        /// <param name="userId">The id of user we are looking for</param>
        /// <returns>A User object</returns>
		User GetUserByID(string userId);

		/// <summary>
        /// Post a User
        /// </summary>
        /// <param name="user">User object to be posted</param>
		void InsertUser(User user);

        /// <summary>
        /// Delete a User
        /// </summary>
        /// <param name="userId">Id of User to be deleted</param>
		void DeleteUser(int userId);

		/// <summary>
        /// Put user
        /// </summary>
        /// <param name="userId">Id of user to edit</param>
        /// <param name="user">Edited User object</param>
		void UpdateUser(string userId, User user);

        /// <summary>
        /// Save to db
        /// </summary>
		void Save();
	}
}