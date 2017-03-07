using System.Threading;
using System.Threading.Tasks;
using Dapplo.Log;
using Dapplo.Owin;
using Owin;

namespace Wapplo.ShareContext
{
	/// <summary>
	/// Configure OWin if needed
	/// </summary>
	[OwinModule]
	public class ShareContextOwinModule : IOwinModule
	{
		private static readonly LogSource Log = new LogSource();
		public Task InitializeAsync(IOwinServer server, CancellationToken cancellationToken = new CancellationToken())
		{
			Log.Verbose().WriteLine("Initializing.");
			return Task.FromResult(true);
		}

		/// <summary>
		///     Here a file server can be created
		/// </summary>
		/// <param name="server">IOwinServer</param>
		/// <param name="appBuilder">IAppBuilder</param>
		public void Configure(IOwinServer server, IAppBuilder appBuilder)
		{
		}

		public Task DeinitializeAsync(IOwinServer server, CancellationToken cancellationToken = new CancellationToken())
		{
			return Task.FromResult(true);
		}
	}
}
