namespace API.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Subclass of User representing an employee following a particular workflow of tasks.
    /// </summary>
    public class Employee : User
    {
        public List<Task> Tasks { get; set; }

        public string Workflow { get; set; }

    }
}