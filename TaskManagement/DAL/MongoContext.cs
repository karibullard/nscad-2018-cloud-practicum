using API.Models;
using MongoDB.Driver;
using System.Configuration;

namespace API.DAL
{
    /// <summary>
    /// Connects MongoContext to the Repository.
    /// </summary>
    public class MongoContext
    {
        /// <summary>
        /// This is an object of IMongoDB
        /// </summary>
        private IMongoDatabase database;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoContext"/> class.
        /// This constructs the context with new client and database
        /// </summary>
        public MongoContext()
        {
            // create an instance of mongo client using connection string to mongo TaskManager db
            var mongoClient = new MongoClient(ConfigurationManager.AppSettings["MongoDbHost"]);

            // get TaskManager database from client connection
            database = mongoClient.GetDatabase(ConfigurationManager.AppSettings["MongoDbName"]);
        }

        /// <summary>
        /// Gets Users collection.
        /// </summary>
        public IMongoCollection<User> Users => database.GetCollection<User>(ConfigurationManager.AppSettings["MongoCollectionName"]);
    }
}