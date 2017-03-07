﻿//  Dapplo - building blocks for desktop applications
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

using System.Threading;
using System.Threading.Tasks;
using Dapplo.Log;
using Dapplo.Owin;
using Owin;

#endregion

namespace Wapplo.ShareContext
{
	/// <summary>
	///     Configure OWin if needed
	/// </summary>
	[OwinModule]
	public class ShareContextOwinModule : IOwinModule
	{
		private static readonly LogSource Log = new LogSource();

		/// <summary>
		///     This is called when initializing the Share Context Owin module
		/// </summary>
		/// <param name="server"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public Task InitializeAsync(IOwinServer server, CancellationToken cancellationToken = new CancellationToken())
		{
			Log.Verbose().WriteLine("Initializing.");
			return Task.FromResult(true);
		}

		/// <summary>
		///     This configures Owin for this module, e.g. here a file server can be created
		/// </summary>
		/// <param name="server">IOwinServer</param>
		/// <param name="appBuilder">IAppBuilder</param>
		public void Configure(IOwinServer server, IAppBuilder appBuilder)
		{
		}

		/// <summary>
		///     This is called when the Share Context Owin module is deinitialized
		/// </summary>
		/// <param name="server"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public Task DeinitializeAsync(IOwinServer server, CancellationToken cancellationToken = new CancellationToken())
		{
			return Task.FromResult(true);
		}
	}
}