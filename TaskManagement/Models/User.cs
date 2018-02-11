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

        [BsonElement("Type")]
        public string Type {get; set;}

        [BsonElement("Employees")]
        public string[] Employees { get; set; }

        [BsonElement("Workflow")]
        public string Workflow { get; set; }

        [BsonElement("Tasks")]
        public string[] Tasks { get; set; }

       //[BsonElement("StartDate")]
       //public string StartDate { get; set; }
   }
}