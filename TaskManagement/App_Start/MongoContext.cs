using MongoDB.Driver;
using API.Models; 

namespace TaskManagement.App_Start
{

    /// <summary>
    /// This class stores the context to collections
    /// </summary>
    public class MongoContext
    {
        public IMongoDatabase DataBase; 

        /// <summary>
        /// Creates client and gets database
        /// </summary>
        public MongoContext()
        {
            //create an instance of mongo client using connection string to mongo TaskManager db
            var client = new MongoClient("mongodb://localhost:27017/TaskManager");
            //get TaskManager database from client connection 
            DataBase = client.GetDatabase("TaskManager"); 


        }
        //retrieve  user collection from database change this based on wanted collection
        public IMongoCollection<User> Users => DataBase.GetCollection<User>("user_main");


    }//end class TaskManagerContext
}//end namespace