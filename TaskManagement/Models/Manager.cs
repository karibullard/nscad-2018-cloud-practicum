namespace API.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Subclass of User representing a single manager overseeing the progress of multiple employees.
    /// </summary>
    public class Manager : User
    {
        /// <summary>
        /// Set of employees that report to a particular manager. 
        /// </summary>
        public Dictionary<string,string> Employees { get; set; }
    }
}