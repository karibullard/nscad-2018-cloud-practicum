using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
	/// <summary>
	/// A configuration document for a UTC on-boarding workflow. A workflow document defines all the
	/// steps a new hire must complete to be considered officially “on-boarded.” An on-boarding
	/// blueprint if you will.
	/// </summary>
	public class Workflow
	{
		/// <summary>
		/// Gets or sets workflow id
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets workflow name
		/// </summary>
		[Required]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets workflow description
		/// </summary>
		[Required]
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets a workflow's "tasks" list
		/// </summary>
		[Required]
		public List<WorkflowTask> Tasks { get; set; }
	}
}