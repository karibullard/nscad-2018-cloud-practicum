using System.Collections.Generic;

namespace API.Models
{
	public class Employee : User
	{
		public List<Task> Tasks { get; set; }

		public string Workflow { get; set; }
	}
}