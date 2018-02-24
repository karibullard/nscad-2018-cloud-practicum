namespace API.Models
{
    using System.Collections.Generic;

    public class Employee : User
    {
        public List<Task> Tasks { get; set; }

        public string Workflow { get; set; }

    }
}