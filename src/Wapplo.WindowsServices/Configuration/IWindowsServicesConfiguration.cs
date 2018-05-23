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

using Dapplo.Ini;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Wapplo.WindowsServices.Configuration
{
	/// <summary>
	/// Configuration for the windows services
	/// </summary>
	[IniSection("WindowsServices")]
	[Description("The configuration for the windows services module")]
	public interface IWindowsServicesConfiguration : IIniSection
	{
		/// <summary>
		/// Specify if web applications are allowed to monitor the clipboard, default is false
		/// </summary>
		[DefaultValue(false), Description("Specify if web applications are allowed to monitor the clipboard"), DataMember(EmitDefaultValue = true)]
		bool AllowClipboardMonitoring { get; set; }
	}
}
