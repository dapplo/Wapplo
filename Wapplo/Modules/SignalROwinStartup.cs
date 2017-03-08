//  Dapplo - building blocks for desktop applications
//  Copyright (C) 2016-2017 Dapplo
// 
//  For more information see: http://dapplo.net/
//  Dapplo repositories are hosted on GitHub: https://github.com/dapplo
// 
//  This file is part of Wapplo
// 
//  Wapplo is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  Wapplo is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have a copy of the GNU Lesser General Public License
//  along with Wapplo. If not, see <http://www.gnu.org/licenses/lgpl.txt>.

#region using

using System.ComponentModel.Composition;
using System.Net;
using Dapplo.Addons;
using Dapplo.Log;
using Dapplo.Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Owin;
using Wapplo.Configuration;
using Wapplo.Utils;

#endregion

namespace Wapplo.Modules
{
	/// <summary>
	///     SignalR generic OWIN configuration
	/// </summary>
	[OwinModule]
	public class SignalROwinStartup : SimpleOwinModule
	{
		private static readonly LogSource Log = new LogSource();

		[Import]
		private IWebserverConfiguration WebserverConfiguration { get; set; }

		[Import]
		private IServiceLocator ServiceLocator { get; set; }

		/// <summary>
		///     Configure OWIN with:
		///     * IntegratedWindowsAuthentication
		///     * SignalR
		///     * CORS
		/// </summary>
		/// <param name="server"></param>
		/// <param name="appBuilder"></param>
		public override void Configure(IOwinServer server, IAppBuilder appBuilder)
		{
			// Register the SignalRContractResolver, which solves camelCase issues
			var settings = new JsonSerializerSettings
			{
				ContractResolver = new SignalRContractResolver()
			};
			var serializer = JsonSerializer.Create(settings);
			GlobalHost.DependencyResolver.Register(typeof(JsonSerializer), () => serializer);

			// Enable Authentication, if a scheme is set
			if (WebserverConfiguration.AuthenticationScheme != AuthenticationSchemes.None)
			{
				var listener = (HttpListener)appBuilder.Properties[typeof(HttpListener).FullName];
				listener.AuthenticationSchemes = WebserverConfiguration.AuthenticationScheme;
			}

			appBuilder.UseErrorPage();

			// Handle errors like 404 (not founds)
			appBuilder.Use((owinContext, next) =>
			{
				return next().ContinueWith(x =>
				{
					if (owinContext.Response.StatusCode < 400)
					{
						return;
					}
					var request = owinContext.Request;
					var statusCode = owinContext.Response.StatusCode;
					Log.Warn().WriteLine("{1} -> {0}", statusCode, request.Uri.AbsolutePath);
					owinContext.Response.Redirect(owinContext.Request.Uri.AbsoluteUri.Replace(request.Uri.PathAndQuery, request.PathBase + $"/error/{statusCode}.html"));
				});
			});

			//// Add the file server for the error pages
			//appBuilder.UseFileServer(new FileServerOptions
			//{
			//	EnableDefaultFiles = false,
			//	RequestPath = new PathString("/error"),
			//	FileSystem = new EmbeddedResourceFileSystem(GetType().Assembly, "CallIng.Error"),
			//	EnableDirectoryBrowsing = false
			//});


			// Enable CORS (allow cross domain requests)
			appBuilder.UseCors(CorsOptions.AllowAll);

			// Add Signal R and enable detailed errors
			appBuilder.MapSignalR(new HubConfiguration
			{
				EnableJavaScriptProxies = true,
				EnableDetailedErrors = true,
				Resolver = new DapploSignalRDependencyResolver(ServiceLocator)
			});
		}
	}
}