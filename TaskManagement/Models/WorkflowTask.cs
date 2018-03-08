using System.Collections.Generic;
using System.Runtime.Serialization;

namespace API.Models
{
	[DataContract]
	public class WorkflowTask
	{
		/// <summary>
		/// Gets or sets id of task
		/// </summary>
		[DataMember]
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets name of task
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets
		/// </summary>
		[DataMember]
		public Dictionary<UserType, string> Descriptions { get; set; }

		/// <summary>
		/// Gets or sets List of Viewers
		/// </summary>
		[DataMember]
		public List<UserType> Viewers { get; set; }
	}
}