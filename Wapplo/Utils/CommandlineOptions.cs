using System.Collections.Generic;
using System.IO;
using CommandLine;
using Dapplo.Ini;
using Wapplo.Modules;

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
		/// Hostname to accept request on.
		/// </summary>
		[Option("hostname", Default = "localhost", HelpText = "Hostname to accept request on.")]
		public string Hostname { get; set; }

		/// <summary>
		/// Port to accept request on.
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
		/// Process the arguments
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
				var parseResult = parser.ParseArguments(() => new CommandlineOptions { Port = WebserverConfiguration.Port, Hostname = WebserverConfiguration.Hostname },
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
