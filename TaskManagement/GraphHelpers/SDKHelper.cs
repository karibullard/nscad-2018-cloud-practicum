using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http.Headers;
using Microsoft.Graph;

namespace API.GraphHelpers
{
    /// <summary>
    /// SDKHelper allows the api app to request a token to grant access to the Graph API.
    /// Portions of this code copyright (c) Microsoft. All rights reserved. Licensed under the MIT license.
    /// </summary>
    public class SDKHelper
    {
        private static GraphServiceClient graphClient = null;

        // Get an authenticated Microsoft Graph Service client.
        public static GraphServiceClient GetAuthenticatedClient()
        {
            GraphServiceClient graphClient = new GraphServiceClient(
                new DelegateAuthenticationProvider(
                    async (requestMessage) =>
                    {
                        string accessToken = await SampleAuthProvider.Instance.GetUserAccessTokenAsync();

                        // Append the access token to the request.
                        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken);
                    }));
            return graphClient;
        }

        public static void SignOutClient()
        {
            graphClient = null;
        }
    }
}