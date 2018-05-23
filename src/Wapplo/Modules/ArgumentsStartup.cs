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

using System;
using System.Windows;
using Dapplo.Addons;
using Wapplo.Configuration;
using Wapplo.Utils;

#endregion

namespace Wapplo.Modules
{
	/// <summary>
	///     Process commandline arguments
	/// </summary>
	[ServiceOrder(int.MinValue)]
	public class ArgumentsStartup : IStartup
	{
	    private readonly IWebserverConfiguration _webserverConfiguration;

        /// <summary>
        /// Constructor for all dependencies
        /// </summary>
        /// <param name="webserverConfiguration">IWebserverConfiguration</param>
        public ArgumentsStartup(
            IWebserverConfiguration webserverConfiguration
            )
	    {
	        _webserverConfiguration = webserverConfiguration;
	    }

		/// <summary>
		///     Perform a start of whatever needs to be started.
		///     Make sure this can be called multiple times, e.g. do nothing when it was already started.
		/// </summary>
		public void Start()
		{
			var commandlineOptions = CommandlineOptions.HandleArguments(Environment.GetCommandLineArgs());
			if (commandlineOptions == null)
			{
				// Parse error, or help requested.
				// As the output is already shown, we can just "exit"
				Application.Current.Shutdown();
				return;
			}
            // Store the result back to the IOwinConfiguration
		    _webserverConfiguration.Hostname = commandlineOptions.Hostname;
		    _webserverConfiguration.Port = commandlineOptions.Port;
		}
	}
}