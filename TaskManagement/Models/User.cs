using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace API.Models {

    [DataContract]
    public class User 
    {

        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public ObjectId Id { get; set; }

        [Required]
        [BsonElement("FirstName")]
        public string FirstName { get; set; }


        [Required]
        [BsonElement("LastName")]
        public string LastName { get; set; }


        [BsonElement("Email")]
        public string Email { get; set; }


        [BsonElement("Phone")]
        public string Phone { get; set; }


        public enum UserType {

            [BsonElement("Employee")]
            EmployeeEnum = 1,

            [BsonElement("Manager")]
            ManagerEnum = 2
        }


        [BsonElement("Type")]
        public UserType? Type { get; set; }

        [BsonElement("StartDate")]
        public string StartDate { get; set; }

    }
}