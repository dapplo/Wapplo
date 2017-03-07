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
using Dapplo.Ini;
using Dapplo.Owin;
using System.Net;

#endregion

namespace Wapplo.Modules
{
	/// <summary>
	///     The configuration for the web-server (owin)
	/// </summary>
	[IniSection("Webserver")]
	[Description("The configuration for the web-server (owin)")]
	public interface IWebserverConfiguration : IIniSection, IOwinConfiguration
	{
		/// <summary>
		/// Specify what AuthenticationScheme is used
		/// </summary>
		[Description("Used Authentication scheme"), DefaultValue(AuthenticationSchemes.None)]
		AuthenticationSchemes AuthenticationScheme { get; set; }
	}
}