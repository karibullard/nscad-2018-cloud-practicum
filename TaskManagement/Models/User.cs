namespace API.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// Parent class representing a single end-user in the api.
    /// </summary>
    [DataContract]
    public class User
    {
        /// <summary>
        /// Unique id for user in db
        /// </summary>
        [DataMember]
        [BsonId]
        public ObjectId Id { get; set; }

        /// <summary>
        /// Each user is linked by the ActiveDirectoryId to an Azure Active Directory instance. 
        /// </summary>
        [DataMember]
        [Required]
        [BsonElement("activeDirectoryId")]
        public string ActiveDirectoryId { get; set; }

        /// <summary>
        /// The user's first name
        /// </summary>
        [DataMember]
        [Required]
        [BsonElement("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// The user's last name
        /// </summary>
        [DataMember]
        [Required]
        [BsonElement("lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Email address of the user
        /// </summary>
        [DataMember]
        [Required]
        [BsonElement("email")]
        public string Email { get; set; }

        /// <summary>
        /// Phone number of the user
        /// </summary>
        [DataMember]
        [BsonElement("phone")]
        [Required]
        public string Phone { get; set; }

        /// <summary>
        /// The type of the user, currently Employee or Manager. Determines permissions and actions.
        /// </summary>
        [DataMember]
        [Required]
        [BsonElement("type")]
        public string Type { get; set; }

        /// <summary>
        /// If the user is a manager, it has a set of related employees. A manager oversees the progress of a set of employees.
        /// </summary>
        [DataMember]
        [BsonElement("employees")]
        public Dictionary<string, string> Employees { get; set; }

        /// <summary>
        /// The workflow a particular employee user is assigned to.
        /// </summary>
        [DataMember]
        [BsonElement("workflow")]
        public string Workflow { get; set; }

        /// <summary>
        /// The set of tasks that an employee user is completing.
        /// </summary>
        [DataMember]
        [BsonElement("tasks")]
        public List<string> Tasks { get; set; }

        /// <summary>
        /// The user's start date a the company
        /// </summary>
        [BsonElement("startDate")]
        public string StartDate { get; set; }
    }
}
