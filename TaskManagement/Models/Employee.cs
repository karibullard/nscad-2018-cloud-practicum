namespace API.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Subclass of User representing an employee following a particular workflow of tasks.
    /// </summary>
    public class Employee : User
    {
        /// <summary>
        /// The set of tasks that a particular employee is working on or has completed.
        /// </summary>
        public List<Task> Tasks { get; set; }

        /// <summary>
        /// The workflow to which an employee is assigned. This will describe the full set of tasks required of them.
        /// </summary>
        public string Workflow { get; set; }

    }
}