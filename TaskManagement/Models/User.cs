using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace API.Models
{
    /// <summary>
    /// Parent class representing a single end-user in the api.
    /// </summary>
	[DataContract]
	public class User
	{
        /// <summary>
        /// Gets or sets unique id for user in db
        /// </summary>
		[DataMember(Name = "id")]
		[BsonId]
        [JsonConverter(typeof(ObjectIdConverter))]
        public ObjectId Id { get; set; }

        /// <summary>
        /// Gets or sets each user is linked by the ActiveDirectoryId to an Azure Active Directory instance.
        /// </summary>
		[DataMember]
		[Required]
		[BsonElement("activeDirectoryId")]
		public string ActiveDirectoryId { get; set; }

        /// <summary>
        /// Gets or sets the user's first name
        /// </summary>
		[DataMember(Name = "firstName")]
		[Required]
		[BsonElement("firstName")]
		public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user's last name
        /// </summary>
		[DataMember(Name = "lastName")]
		[Required]
		[BsonElement("lastName")]
		public string LastName { get; set; }

        /// <summary>
        /// Gets or sets email address of the user
        /// </summary>
		[DataMember(Name = "email")]
		[Required]
		[BsonElement("email")]
		public string Email { get; set; }

        /// <summary>
        /// Gets or sets phone number of the user
        /// </summary>
		[DataMember(Name = "phone")]
		[BsonElement("phone")]
		[Required]
		public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the type of the user, currently Employee or Manager. Determines permissions and actions.
        /// </summary>
		[DataMember(Name = "type")]
		[Required]
		[BsonElement("type")]
		public string Type { get; set; }

        /// <summary>
        /// Gets or sets if the user is a manager, it has a set of related employees. A manager oversees the progress of a set of employees.
        /// </summary>
		[DataMember(Name = "employees")]
		[BsonElement("employees")]
		public Dictionary<string, string> Employees { get; set; }

        /// <summary>
        /// Gets or sets the workflow a particular employee user is assigned to.
        /// </summary>
		[DataMember]
		[BsonElement("workflow")]
		public string Workflow { get; set; }

        /// <summary>
        /// Gets or sets the set of tasks that an employee user is completing.
        /// </summary>
		[DataMember]
		[BsonElement("tasks")]
		public List<string> Tasks { get; set; }

        /// <summary>
        /// Gets or sets the user's start date a the company
        /// </summary>
        [DataMember]
		[BsonElement("startDate")]
		public string StartDate { get; set; }
	}
}
