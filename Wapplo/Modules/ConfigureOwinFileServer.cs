using System.ComponentModel.Composition;
using System.Reflection;
using Dapplo.Log;
using Dapplo.Owin;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using Wapplo.Configuration;

namespace Wapplo.Modules
{
	/// <summary>
	///     An abstract Owin Module which makes it easier to configure a file server
	/// </summary>
	[OwinModule]
	public class ConfigureOwinFileServer : BaseOwinModule
	{
		private static readonly LogSource Log = new LogSource();

		/// <summary>
		/// IOwinFileServerConfiguration
		/// </summary>
		[Import]
		protected IWebserverConfiguration WebserverConfiguration { get; set; }

		/// <summary>
		///     Configure FileServer for Owin
		/// </summary>
		/// <param name="server">IOwinServer</param>
		/// <param name="appBuilder">IAppBuilder</param>
		public override void Configure(IOwinServer server, IAppBuilder appBuilder)
		{
			Log.Verbose().WriteLine("Enabling file server: {0}", WebserverConfiguration.EnableFileServer);
			if (!WebserverConfiguration.EnableFileServer)
			{
				return;
			}

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
					owinContext.Response.Redirect(owinContext.Request.Uri.AbsoluteUri.Replace(request.Uri.PathAndQuery, request.PathBase + $"/html/error/{statusCode}.html"));
				});
			});

			switch (WebserverConfiguration.FileServerType)
			{
				case FileServerTypes.Embedded:
					ConfigureEmbeddedFileServer(appBuilder, WebserverConfiguration.FileServerPath, Assembly.GetExecutingAssembly(), WebserverConfiguration.FileServerLocation);
					break;
				case FileServerTypes.Physical:
					ConfigurePhysicalFileServer(appBuilder, WebserverConfiguration.FileServerPath, WebserverConfiguration.FileServerLocation);
					break;
			}

		}

		/// <summary>
		/// Configure a physical file server
		/// </summary>
		/// <param name="appBuilder">IAppBuilder</param>
		/// <param name="path"></param>
		/// <param name="root"></param>
		/// <param name="enableDefaultFiles"></param>
		/// <param name="enableDirectoryBrowsing">Enable if a browser can query the content, default false</param>
		protected void ConfigurePhysicalFileServer(IAppBuilder appBuilder, string path, string root, bool enableDefaultFiles = true, bool enableDirectoryBrowsing = false)
		{
			appBuilder.UseFileServer(new FileServerOptions
			{
				EnableDefaultFiles = enableDefaultFiles,
				RequestPath = new PathString(WebserverConfiguration.FileServerPath),
				FileSystem = new PhysicalFileSystem(root),
				EnableDirectoryBrowsing = enableDirectoryBrowsing
			});
		}

		/// <summary>
		/// Configure an embedded file server
		/// </summary>
		/// <param name="appBuilder">IAppBuilder</param>
		/// <param name="path"></param>
		/// <param name="assemblyWithResoures"></param>
		/// <param name="namespacePath"></param>
		/// <param name="enableDefaultFiles">Enable if</param>
		/// <param name="enableDirectoryBrowsing">Enable if a browser can query the content, default false</param>
		protected void ConfigureEmbeddedFileServer(IAppBuilder appBuilder, string path, Assembly assemblyWithResoures, string namespacePath = null, bool enableDefaultFiles = true, bool enableDirectoryBrowsing = false)
		{
			appBuilder.UseFileServer(new FileServerOptions
			{
				EnableDefaultFiles = enableDefaultFiles,
				RequestPath = new PathString(path),
				FileSystem = new EmbeddedResourceFileSystem(assemblyWithResoures, namespacePath),
				EnableDirectoryBrowsing = enableDirectoryBrowsing
			});
		}
	}
}
