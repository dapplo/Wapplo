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

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Subjects;
using Microsoft.AspNet.SignalR;
using Wapplo.WindowsServices.Configuration;

namespace Wapplo.WindowsServices.Hub
{
	/// <summary>
	/// The Signal-R hub for making Windows services available
	/// </summary>
	public class WindowsServicesHub : Hub<IWindowsServicesClient>, IWindowsServicesServer
	{
		private static readonly IDictionary<string, IDisposable> ClipboardSubscriptions = new ConcurrentDictionary<string, IDisposable>();

		[Import]
		private IWindowsServicesConfiguration WindowsServicesConfiguration { get; set; }

		[Import("ClipboardUpdates")]
		private ISubject<IEnumerable<string>> ClipboardUpdates { get; set; }

		/// <summary>
		/// Implement the CopyToClipboard service
		/// </summary>
		/// <param name="origin">string with the information where the clipboard comes from</param>
		/// <param name="text">The actual string</param>
		public void CopyToClipboard(string origin, string text)
		{
			throw new NotSupportedException();
		}

		/// <summary>
		/// Have the client monitor clipboard changes
		/// </summary>
		/// <param name="enable">bool</param>
		public void MonitorClipboard(bool enable)
		{
			if (!WindowsServicesConfiguration.AllowClipboardMonitoring)
			{
				throw new NotSupportedException();
			}

			if (enable)
			{
				if (ClipboardSubscriptions.ContainsKey(Context.ConnectionId))
				{
					// Do nothing, already subscribed
					return;
				}
				// Create a subscription, which will inform of clipboard updates
				var subscription = ClipboardUpdates.Subscribe(formats =>
				{
					Clients.Client(Context.ConnectionId).ClipboardChanged(formats.ToList());
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
