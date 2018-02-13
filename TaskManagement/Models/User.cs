using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

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
        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [DataMember]
        [Required]
        [BsonElement("LastName")]
        public string LastName { get; set; }

        [DataMember]
        [BsonElement("Email")]
        public string Email { get; set; }

        [DataMember]
        [BsonElement("Phone")]
        public string Phone { get; set; }

        [DataMember]
        [BsonElement("Type")]
        public string Type { get; set; }

        [DataMember]
        [BsonElement("Employees")]
        public string[] Employees { get; set; }

        [DataMember]
        [BsonElement("Workflow")]
        public string Workflow { get; set; }

        [DataMember]
        [BsonElement("Tasks")]
        public string[] Tasks { get; set; }

        //[BsonElement("StartDate")]
        //public string StartDate { get; set; }
    }
}