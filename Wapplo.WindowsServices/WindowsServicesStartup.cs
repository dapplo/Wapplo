using System;
using System.Collections.Generic;
using Dapplo.Addons;
using Wapplo.WindowsServices.Configuration;
using System.ComponentModel.Composition;
using System.Reactive.Subjects;
using Dapplo.Windows.Clipboard;

namespace Wapplo.WindowsServices
{
	/// <summary>
	/// Make it possible to subscribe to events
	/// </summary>
	[StartupAction, ShutdownAction]
	public class WindowsServicesStartup : IStartupAction, IShutdownAction
	{
		[Import]
		private IWindowsServicesConfiguration WindowsServicesConfiguration { get; set; }

		[Export("ClipboardUpdates")]
		private ISubject<IEnumerable<string>> ClipboardUpdates { get; } = new Subject<IEnumerable<string>>();

		private IDisposable _clipboardMonitor;

		/// <summary>
		/// Start will register all needed services
		/// </summary>
		public void Start()
		{
			if (false && WindowsServicesConfiguration.AllowClipboardMonitoring)
			{
				// TODO: Fix STA Thread needed
				_clipboardMonitor = ClipboardMonitor.ClipboardUpdateEvents.Subscribe(args =>
				{
					ClipboardUpdates.OnNext(args.Formats);
				});

			}
		}

		/// <summary>
		/// Dispose the Subscription when stopping
		/// </summary>
		public void Shutdown()
		{
			_clipboardMonitor?.Dispose();
		}
	}
}
