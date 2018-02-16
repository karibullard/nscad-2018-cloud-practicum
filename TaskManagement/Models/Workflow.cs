namespace API.Models
{
    using System.Collections.Generic;

    public class Workflow
    {
        public Workflow()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Task> Tasks { get; set; }
    }
}