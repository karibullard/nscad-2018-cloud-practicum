using System.Collections.Generic;

namespace API.Models
{
	public class Manager : User
	{
		public Dictionary<string, string> Employees { get; set; }
	}
}