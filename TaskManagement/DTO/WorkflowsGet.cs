using System.Runtime.Serialization;

namespace API.Responses
{/// <summary>
 /// A data transfer object for the /workflow GET endpoint. </summary>
	[DataContract]
	public class WorkflowsGet
	{
		/// <summary>
		/// The workflow's human-readable "display" name.
		/// </summary>
		[DataMember]
		private string name;

		/// <summary>
		/// The workflow's id. Used later to retrieve the full instruction set.
		/// </summary>
		[DataMember]
		private string id;

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkflowsGet"/> class.
		/// </summary>
		/// <param name="id">The workflow id.</param>
		/// <param name="name">The workflow name.</param>
		/// <example>
		/// <code>
		/// JSON Example Model:
		///
		///  {
		///   "name": "Cloud Offshore External",
		///   "id": "CloudOffshoreExternal"
		///   }
		/// </code>
		/// </example>
		public WorkflowsGet(string id, string name)
		{
			this.name = name;
			this.id = id;
		}

		internal string Name { get => this.name; set => this.name = value; }

		internal string Id { get => this.id; set => this.id = value; }
	}
}