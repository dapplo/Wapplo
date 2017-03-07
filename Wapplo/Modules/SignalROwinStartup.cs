using System.Net;
using Dapplo.Log;
using Dapplo.Owin;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Owin;
using Wapplo.Utils;

namespace Wapplo.Modules
{
	/// <summary>
	///     SignalR generic OWIN configuration
	/// </summary>
	public class SignalROwinStartup : SimpleOwinModule
	{
		private static readonly LogSource Log = new LogSource();

		/// <summary>
		/// Configure OWIN with: 
		/// * IntegratedWindowsAuthentication
		/// * SignalR
		/// * CORS
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

			// Enable Windows-Authentication
			var listener = (HttpListener)appBuilder.Properties[typeof(HttpListener).FullName];
			listener.AuthenticationSchemes = AuthenticationSchemes.Negotiate;

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
			appBuilder.MapSignalR(new HubConfiguration {
				EnableJavaScriptProxies = true,
				EnableDetailedErrors = true
			});
		}
	}
}
