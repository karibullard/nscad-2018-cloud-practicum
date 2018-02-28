using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace API.Models
{
	[DataContract]
	public class User
	{
		[DataMember]
		[BsonId]
		public ObjectId Id { get; set; }

		[DataMember]
		[Required]
		[BsonElement("activeDirectoryId")]
		public string ActiveDirectoryId { get; set; }

		[DataMember]
		[Required]
		[BsonElement("firstName")]
		public string FirstName { get; set; }

		[DataMember]
		[Required]
		[BsonElement("lastName")]
		public string LastName { get; set; }

		[DataMember]
		[Required]
		[BsonElement("email")]
		public string Email { get; set; }

		[DataMember]
		[BsonElement("phone")]
		[Required]
		public string Phone { get; set; }

		[DataMember]
		[Required]
		[BsonElement("type")]
		public string Type { get; set; }

		[DataMember]
		[BsonElement("employees")]
		public Dictionary<string, string> Employees { get; set; }

		[DataMember]
		[BsonElement("workflow")]
		public string Workflow { get; set; }

		[DataMember]
		[BsonElement("tasks")]
		public List<string> Tasks { get; set; }

		[BsonElement("startDate")]
		public string StartDate { get; set; }
	}
}