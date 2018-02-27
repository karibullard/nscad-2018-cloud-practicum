namespace API.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Subclass of User representing a single manager overseeing the progress of multiple employees.
    /// </summary>
    public class Manager : User
    {
        public Dictionary<string,string> Employees { get; set; }
    }
}