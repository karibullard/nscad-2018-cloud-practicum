using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using MongoDB.Driver;
using API.Models; 

namespace TaskManagement.App_Start
{

    //this class connects to mongo db using MongoDb driver
    public class MongoContext
    {
        public IMongoDatabase DataBase; 

        public MongoContext()
        {
            //create an instance of mongo client using connection string to mongo TaskManager db
            var client = new MongoClient("mongodb://localhost:27017/TaskManager");
            //get TaskManager database from client connection 
            DataBase = client.GetDatabase("TaskManager"); 


        }
        //retrieve  user collection from database
        public IMongoCollection<User> Users => DataBase.GetCollection<User>("users4"); 


    }//end class TaskManagerContext
}//end namespace