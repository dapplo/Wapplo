using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Dapplo.Windows.Clipboard;
using Microsoft.AspNet.SignalR;

namespace Wapplo.WindowsServices.Hub
{
	/// <summary>
	/// The Signal-R hub for making Windows services available
	/// </summary>
	public class WindowsServicesHub : Hub<IWindowsServicesClient>, IWindowsServicesServer
	{
		private static readonly IDictionary<string, IDisposable> ClipboardSubscriptions = new ConcurrentDictionary<string, IDisposable>();

		public void CopyToClipboard(string origin, string text)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Have the client
		/// </summary>
		/// <param name="enable"></param>
		public void MonitorClipboard(bool enable)
		{
			if (enable)
			{
				if (ClipboardSubscriptions.ContainsKey(Context.ConnectionId))
				{
					// Do nothing, already subscribed
					return;
				}
				// Create a subscription, which will inform of clipboard updates
				var subscription = ClipboardMonitor.ClipboardUpdateEvents.Subscribe(args =>
				{
					Clients.Client(Context.ConnectionId).ClipboardChanged(args.Formats.ToList());
				});
				ClipboardSubscriptions[Context.ConnectionId] = subscription;
			}
			else if (ClipboardSubscriptions.ContainsKey(Context.ConnectionId))
			{
				ClipboardSubscriptions[Context.ConnectionId].Dispose();
			}

		}
	}
}
