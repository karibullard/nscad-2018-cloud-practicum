using MongoDB.Driver;
using API.Models;
using System.Configuration; 

namespace TaskManagement.App_Start
{

    //this class connects to mongo db using MongoDb driver
    public class MongoContext
    {
        /// <summary>
        ///This is an object of IMongoDB
        /// </summary>
        public IMongoDatabase DataBase;

        /// <summary>
        ///This constructs the context with new client and database
        /// </summary>
        public MongoContext()
        {



            //create an instance of mongo client using connection string to mongo TaskManager db
            var mongoClient = new MongoClient(ConfigurationManager.AppSettings["MongoDbHost"]);
           
            //get TaskManager database from client connection 
            DataBase = mongoClient.GetDatabase(ConfigurationManager.AppSettings["MongoDbName"]);


        }
        //retrieve  user collection from database change this based on wanted collection
        public IMongoCollection<User> Users => DataBase.GetCollection<User>("user_main");       
       
    }//end class TaskManagerContext
}//end namespace