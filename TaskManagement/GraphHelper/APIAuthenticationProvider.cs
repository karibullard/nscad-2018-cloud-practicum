using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace API.GraphHelper
{
    public class APIAuthenticationProvider : IAuthenticationProvider
    {
        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            string clientId = "64f3c1c2-201f-4e1a-b0cb-e86c5ab7be4e";
            string clientSecret = "lQtXj/22TfbLvJPGSY5bOozoUxJcOr5UsddCtzhVr5A=";

            AuthenticationContext authContext = new AuthenticationContext("https://login.microsoftonline.com/08c1c649-bfdd-439e-8e5b-5ff31c72ce4e/oauth2/token");

            ClientCredential creds = new ClientCredential(clientId, clientSecret);

            AuthenticationResult authResult = await authContext.AcquireTokenAsync("https://graph.microsoft.com/", creds);

            request.Headers.Add("Authorization", "Bearer " + authResult.AccessToken);
        }
    }
}