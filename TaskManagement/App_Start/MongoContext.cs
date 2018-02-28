using System.Configuration;
using API.Models;
using MongoDB.Driver;

namespace TaskManagement.App_Start
{
	/// <summary>
	/// Connects MongoContext to the Repository.
	/// </summary>
	public class MongoContext
	{
		/// <summary>
		/// This is an object of IMongoDB
		/// </summary>
		public IMongoDatabase DataBase;

		/// <summary>
		/// Initializes a new instance of the <see cref="MongoContext"/> class. This constructs the
		/// context with new client and database
		/// </summary>
		public MongoContext()
		{
			// create an instance of mongo client using connection string to mongo TaskManager db
			var mongoClient = new MongoClient(ConfigurationManager.AppSettings["MongoDbHost"]);

			// get TaskManager database from client connection
			DataBase = mongoClient.GetDatabase(ConfigurationManager.AppSettings["MongoDbName"]);
		}

		// retrieve user collection from database change this based on wanted collection
		public IMongoCollection<User> Users => DataBase.GetCollection<User>(ConfigurationManager.AppSettings["MongoCollectionName"]);
	}// end class TaskManagerContext
}// end namespace