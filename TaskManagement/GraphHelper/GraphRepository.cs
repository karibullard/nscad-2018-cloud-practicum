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

        public GraphRepository(GraphServiceClient client)
        {
            this.client = client;
        }

        public async Task<String> GetUserID(string userPrincipalName)
        {
            User user = await client.Users[userPrincipalName].Request().GetAsync();
            return user.Id;
        }
    }
}