using System;
using System.ComponentModel.Composition;
using System.Windows;
using Dapplo.Addons;
using Wapplo.Utils;

namespace Wapplo.Modules
{
	/// <summary>
	///    Process commandline arguments
	/// </summary>
	[StartupAction(AwaitStart = true, StartupOrder = int.MinValue + 200)]
	public class ArgumentsStartup : IStartupAction
	{
		[Import]
		private IWebserverConfiguration WebserverConfiguration { get; set; }

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
			WebserverConfiguration.Hostname = commandlineOptions.Hostname;
			WebserverConfiguration.Port = commandlineOptions.Port;
		}
	}
}
