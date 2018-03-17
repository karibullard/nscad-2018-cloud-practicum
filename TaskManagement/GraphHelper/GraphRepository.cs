using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Graph;
using System.Threading.Tasks;

namespace API.GraphHelper
{
    public class GraphRepository
    {
        GraphServiceClient client;

        public GraphRepository()
        {
            APIAuthenticationProvider sampleAuthProvider = new APIAuthenticationProvider();
            this.client = new Microsoft.Graph.GraphServiceClient(sampleAuthProvider);
        }

        public async Task<String> GetUserID(string userPrincipalName)
        {
            User user = await client.Users[userPrincipalName].Request().GetAsync();
            return user.Id;
        }

        public async Task<String> CreateUserID(Models.User newUser)
        {
            User user = await client.Users.Request().AddAsync(new User
            {
                AccountEnabled = true,
                DisplayName = newUser.FirstName + " " + newUser.LastName,
                MailNickname = "API_" + newUser.FirstName,
                PasswordProfile = new PasswordProfile
                {
                    Password = "TempP@ssw0rd!"
                },
                UserPrincipalName = newUser.Email
            });

            return user.Id;
        }

        public async Task<String> FindOrCreateUser(Models.User user)
        {
            string activeDirectoryId;

            try
            {
                activeDirectoryId = await GetUserID(user.Email);
            } catch (ServiceException e)
            {
                activeDirectoryId = await CreateUserID(user);
            }

            return activeDirectoryId;
        }
    }
}