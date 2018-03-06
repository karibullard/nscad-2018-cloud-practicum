using System.Collections.Generic;

namespace API.Models
{
	/// <summary>
	/// Subclass of User representing an employee following a particular workflow of tasks.
	/// </summary>
	public class Employee : User
	{
		/// <summary>
		/// Gets or sets the set of tasks that a particular employee is working on or has completed.
		/// </summary>
		public List<WorkflowTask> Tasks { get; set; }

		/// <summary>
		/// Gets or sets the workflow to which an employee is assigned. This will describe the full
		/// set of tasks required of them.
		/// </summary>
		public string Workflow { get; set; }
	}
}