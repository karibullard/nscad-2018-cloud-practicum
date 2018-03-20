﻿using System.Configuration;
using Microsoft.Owin.Security.ActiveDirectory;
using Owin;

namespace API
{
	public partial class Startup
	{
		// For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
		public void ConfigureAuth(IAppBuilder app)
		{
			app.UseWindowsAzureActiveDirectoryBearerAuthentication(
				new WindowsAzureActiveDirectoryBearerAuthenticationOptions
				{
					Tenant = ConfigurationManager.AppSettings["ida:Tenant"],
					TokenValidationParameters = new System.IdentityModel.Tokens.TokenValidationParameters
					{
						ValidAudience = ConfigurationManager.AppSettings["ida:Audience"]
					},
					MetadataAddress = ConfigurationManager.AppSettings["ida:MetadataAddress"],
				});
		}
	}
}