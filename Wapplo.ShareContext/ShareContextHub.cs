using Dapplo.Log;
using Microsoft.AspNet.SignalR;

namespace Wapplo.ShareContext
{
	/// <summary>
	/// The share context hub
	/// </summary>
	public class ShareContextHub : Hub<IShareContextClient>, IShareContextServer
	{
		private static readonly LogSource Log = new LogSource();
		public void ShareContext(SharingContext sharingContext)
		{
			Log.Verbose().WriteLine("Context shared from {0}", sharingContext.Origin);
			Clients.Others.ContextShared(sharingContext);
		}
	}
}
