using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace API.Models {
    public class Employee : User {

        public List<Task> Tasks { get; set; }
        public string Workflow { get; set; }
        
    }
}