using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace API.Models {
    public class Task {

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        //[BsonElement("Description")]
        //public Dictionary<User.UserType, string> Descriptions { get; set; }

        //[BsonElement("Viewers")]
        //public List<User.UserType> Viewers { get; set; }

    }
}