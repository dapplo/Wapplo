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

using System.ComponentModel;
using System.Net;
using System.Runtime.Serialization;
using Dapplo.Ini;
using Dapplo.Owin;
using Dapplo.SignalR;
using Dapplo.SignalR.Configuration;

#endregion

namespace Wapplo.Configuration
{
	/// <summary>
	///     The configuration for the web-server (owin)
	/// </summary>
	[IniSection("Webserver")]
	[Description("The configuration for the web-server (owin)")]
	public interface IWebserverConfiguration : IIniSection, IOwinConfiguration, ISignalRConfiguration
	{
		/// <summary>
		/// Enable serving of files from a html sub folder, this can be used to allow error pages.
		/// </summary>
		[Description("Enable serving of files from a html sub folder, this can be used to allow error pages"), DefaultValue(true), DataMember(EmitDefaultValue = false)]
		bool EnableFileServer { get; set; }

		/// <summary>
		/// Set the type of file server which is used
		/// </summary>
		[Description("Set the file-server type, can be Embedded or Physical"), DefaultValue(FileServerTypes.Physical), DataMember(EmitDefaultValue = false)]
		FileServerTypes FileServerType { get; set; }

		/// <summary>
		/// Whenever the file server is enabled, this is the uri path location where it responds to
		/// </summary>
		[Description("Uri path where the file server is responding to"), DefaultValue("/html"), DataMember(EmitDefaultValue = false)]
		string FileServerPath { get; set; }

		/// <summary>
		/// Whenever the file server is enabled, this describes where files are located
		/// </summary>
		[Description("Path or namespace where the physical or embedded file server loads the data from."), DefaultValue("Html"), DataMember(EmitDefaultValue = false)]
		string FileServerLocation { get; set; }
	}
}