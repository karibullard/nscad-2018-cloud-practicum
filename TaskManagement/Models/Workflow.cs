namespace API.Models
{
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Bson.Serialization.IdGenerators;

    public class Workflow
    {
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Tasks")]
        public List<Task> Tasks { get; set; }
    }
}