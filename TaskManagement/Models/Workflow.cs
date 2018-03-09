using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace API.Models
{
    /// <summary>
    /// A configuration document for a UTC on-boarding workflow. A workflow document defines all the
    /// steps a new hire must complete to be considered officially “on-boarded.” An on-boarding
    /// blueprint if you will.
    /// </summary>
    [DataContract]
    public class Workflow
	{
        /// <summary>
        /// Gets or sets workflow id
        /// </summary>
        /// <value>Uniquely identifies a workflow.</value>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets workflow name
        /// </summary>
        /// <value>The display name of the workflow.</value>
        [DataMember(Name = "name")]
        [Required]
		public string Name { get; set; }

        /// <summary>
        /// Gets or sets workflow description
        /// </summary>
        /// <value>Describes a workflow&#39;s purpose.</value>
        [Required]
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a workflow's "tasks" list
        /// </summary>
        /// <value>Associates tasks with a workflow and maintains the order in which Tasks should be completed.</value>
        [Required]
        [DataMember(Name = "tasks")]
        public List<WorkflowTask> Tasks { get; set; }
	}
}