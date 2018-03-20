using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;

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
		/// <value>Describes a workflow's purpose.</value>
		[Required]
		[DataMember(Name = "description")]
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets a workflow's "tasks" list
		/// </summary>
		/// <value>
		/// Associates tasks with a workflow and maintains the order in which Tasks should be completed.
		/// </value>
		[Required]
		[DataMember(Name = "tasks")]
		public List<WorkflowTask> Tasks { get; set; }

		#region Operators

#pragma warning disable 1591

		public static bool operator ==(Workflow left, Workflow right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Workflow left, Workflow right)
		{
			return !Equals(left, right);
		}

#pragma warning restore 1591

		#endregion Operators

		/// <summary>
		/// Returns the string presentation of the object
		/// </summary>
		/// <returns>String presentation of the object</returns>
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append("class Workflow {");
			sb.Append("  Id: ").Append(Id);
			sb.Append("  Name: ").Append(Name);
			sb.Append("  Description: ").Append(Description);
			sb.Append("  Tasks: ").Append(Tasks);
			sb.Append("}");
			return sb.ToString();
		}

		/// <summary>
		/// Returns the JSON string presentation of the object
		/// </summary>
		/// <returns>JSON string presentation of the object</returns>
		public string ToJson()
		{
			return JsonConvert.SerializeObject(this, Formatting.Indented);
		}

		/// <summary>
		/// Returns true if objects are equal
		/// </summary>
		/// <param name="obj">Object to be compared</param>
		/// <returns>Boolean</returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			return obj.GetType() == GetType() && Equals((Workflow)obj);
		}

		/// <summary>
		/// Returns true if Workflow instances are equal
		/// </summary>
		/// <param name="other">Instance of Workflow to be compared</param>
		/// <returns>Boolean</returns>
		public bool Equals(Workflow other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return
				(
					Id == other.Id ||
					(Id != null &&
					Id.Equals(other.Id))
				) &&
				(
					Name == other.Name ||
					(Name != null &&
					Name.Equals(other.Name))
				) &&
				(
					Description == other.Description ||
					(Description != null &&
					Description.Equals(other.Description))
				) &&
				(
					Tasks == other.Tasks ||
					(Tasks != null &&
					Tasks.SequenceEqual(other.Tasks))
				);
		}

		/// <summary>
		/// Gets the hash code
		/// </summary>
		/// <returns>Hash code</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = 41;
				if (Id != null)
				{
					hashCode = (hashCode * 59) + Id.GetHashCode();
				}

				if (Name != null)
				{
					hashCode = (hashCode * 59) + Name.GetHashCode();
				}

				if (Description != null)
				{
					hashCode = (hashCode * 59) + Description.GetHashCode();
				}

				if (Tasks != null)
				{
					hashCode = (hashCode * 59) + Tasks.GetHashCode();
				}

				return hashCode;
			}
		}
	}
}