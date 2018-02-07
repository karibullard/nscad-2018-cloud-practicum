using System;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Security.Authentication;
using API.Models;

namespace TaskManagement.DAL {

    /// <summary>
    /// Created This Class Using The Example Here: https://github.com/Azure-Samples/azure-cosmos-db-mongodb-dotnet-getting-started/blob/master/MyTaskListApp/DAL/Dal.cs (Kari)
    /// </summary>
    public class MongoAccess: IDisposable {
        //private MongoServer mongoServer = null;
        private bool disposed = false;

        // To do: update the connection string with the DNS name
        // or IP address of your server. 
        //For example, "mongodb://testlinux.cloudapp.net
        private string userName = "FILLME";
        private string host = "FILLME";
        private string password = "FILLME";

        // This sample uses a database named "Tasks" and a 
        //collection named "TasksList".  The database and collection 
        //will be automatically created if they don't already exist.
        private string dbName = "Tasks";
        private string collectionName = "TasksList";

        // Default constructor.        
        public MongoAccess() {
        }

        // Gets all Task items from the MongoDB server.        
        public List<Task> GetAllTasks() {
            try {
                var collection = GetTasksCollection();
                return collection.Find(new BsonDocument()).ToList();
            } catch(MongoConnectionException) {
                return new List<Task>();
            }
        }

        // Creates a Task and inserts it into the collection in MongoDB.
        public void CreateTask(Task task) {
            var collection = GetTasksCollectionForEdit();
            try {
                collection.InsertOne(task);
            } catch(MongoCommandException ex) {
                string msg = ex.Message;
            }
        }

        private IMongoCollection<Task> GetTasksCollection() {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(host, 10255);
            settings.UseSsl = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);

            settings.Credentials = new List<MongoCredential>()
            {
                new MongoCredential("SCRAM-SHA-1", identity, evidence)
            };

            MongoClient client = new MongoClient(settings);
            var database = client.GetDatabase(dbName);
            var todoTaskCollection = database.GetCollection<Task>(collectionName);
            return todoTaskCollection;
        }

        private IMongoCollection<Task> GetTasksCollectionForEdit() {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(host, 10255);
            settings.UseSsl = true;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            MongoIdentity identity = new MongoInternalIdentity(dbName, userName);
            MongoIdentityEvidence evidence = new PasswordEvidence(password);

            settings.Credentials = new List<MongoCredential>()
            {
                new MongoCredential("SCRAM-SHA-1", identity, evidence)
            };
            MongoClient client = new MongoClient(settings);
            var database = client.GetDatabase(dbName);
            var todoTaskCollection = database.GetCollection<Task>(collectionName);
            return todoTaskCollection;
        }

        # region IDisposable

        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if(!this.disposed) {
                if(disposing) {
                }
            }

            this.disposed = true;
        }

        # endregion
    }
}