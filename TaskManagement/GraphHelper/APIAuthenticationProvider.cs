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
            string clientId = "ae798feb-2a57-4738-8037-2e7d57ac6930";
            string clientSecret = "84fxdktLxiXvax0iI/m1ARB+QaEpd2c8jZD6tQK9Alc=";

            AuthenticationContext authContext = new AuthenticationContext("https://login.windows.net/jonhussdev.onmicrosoft.com/oauth2/token");

            ClientCredential creds = new ClientCredential(clientId, clientSecret);

            AuthenticationResult authResult = await authContext.AcquireTokenAsync("https://graph.microsoft.com/", creds);

            request.Headers.Add("Authorization", "Bearer " + authResult.AccessToken);
        }
    }
}