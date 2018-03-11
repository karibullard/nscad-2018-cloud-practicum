using System.Runtime.Serialization;

namespace API.DTO
{
    /// <summary>
    /// UserGet DTO
    /// </summary>
    [DataContract]
    public class UserGet
    {
        /// <summary>
        /// Gets or sets dTO User first and last name
        /// </summary>
        [DataMember]
        private string name;

        /// <summary>
        /// Gets or sets dTO user ADID
        /// </summary>
        [DataMember]
        private string activeDirectoryId;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserGet"/> class.
        /// Constructor for UserGet DTO
        /// </summary>
        /// <param name="firstName">First name of User</param>
        /// <param name="lastName">Last Name of User</param>
        /// <param name="activeDirectoryId">AD Id of User</param>
        public UserGet(string firstName, string lastName, string activeDirectoryId)
        {
            this.name = firstName + " " + lastName;
            this.activeDirectoryId = activeDirectoryId;
        }

        internal string Name { get => this.name; set => this.name = value; }

        internal string ActiveDirectoryId { get => this.activeDirectoryId; set => this.activeDirectoryId = value; }
    }

}