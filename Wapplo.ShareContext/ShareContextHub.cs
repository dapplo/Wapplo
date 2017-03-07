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

using Dapplo.Log;
using Microsoft.AspNet.SignalR;

#endregion

namespace Wapplo.ShareContext
{
	/// <summary>
	///     The share context hub
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