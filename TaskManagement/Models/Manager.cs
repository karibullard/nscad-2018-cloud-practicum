namespace API.Models
{
    using System.Collections.Generic;

    public class Manager : User
    {
        public Dictionary<string,string> Employees { get; set; }
    }
}