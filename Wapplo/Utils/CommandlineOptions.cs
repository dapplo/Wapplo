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

using System.Collections.Generic;
using System.IO;
using CommandLine;
using Dapplo.Ini;
using Wapplo.Configuration;

#endregion

namespace Wapplo.Utils
{
	/// <summary>
	///     Configuration of the commandline options.
	///     default the help and version arguments are already supplied.
	/// </summary>
	public class CommandlineOptions
	{
		private static readonly IWebserverConfiguration WebserverConfiguration = IniConfig.Current.Get<IWebserverConfiguration>();

		/// <summary>
		///     Hostname to accept request on.
		/// </summary>
		[Option("hostname", Default = "localhost", HelpText = "Hostname to accept request on.")]
		public string Hostname { get; set; }

		/// <summary>
		///     Port to accept request on.
		/// </summary>
		[Option("port", Default = 9277, HelpText = "Port to accept request on, default is WAPP => 9277.")]
		public int Port { get; set; }

		/// <summary>
		///     The uri is the processed uri
		/// </summary>
		public string Uri => $"http://{Hostname}:{Port}";

		/// <summary>
		///     Specifying this option will make the application output verbose information
		/// </summary>
		[Option(HelpText = "Print details during execution.")]
		public bool Verbose { get; set; }

		/// <summary>
		///     Process the arguments
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static CommandlineOptions HandleArguments(IEnumerable<string> args)
		{
			CommandlineOptions result;

			using (var writer = new StringWriter())
			{
				var parser = new Parser(config =>
				{
					if (writer != null)
					{
						config.HelpWriter = writer;
					}
				});
				var parseResult = parser.ParseArguments(() => new CommandlineOptions {Port = WebserverConfiguration.Port, Hostname = WebserverConfiguration.Hostname},
					args);
				result = parseResult.MapResult(parsed => parsed, nonParsed => null);
				// TODO: Handle some options
				if (result?.Verbose == true)
				{
					writer.WriteLine($"Listening on {result.Uri}");
				}
				ConsoleOutput.Show(writer.ToString());
			}
			if (result == null)
			{
				ConsoleOutput.Close();
			}
			return result;
		}
	}
}