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

using System;
using System.Collections.Generic;
using Dapplo.Addons;
using Wapplo.WindowsServices.Configuration;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using Dapplo.Log;
using Dapplo.Windows.Clipboard;

namespace Wapplo.WindowsServices
{
    /// <summary>
    /// Make it possible to subscribe to events
    /// </summary>
    [StartupAction, ShutdownAction]
    public class WindowsServicesStartup : IStartupAction, IShutdownAction
    {
        private static readonly LogSource Log = new LogSource();
        private readonly IWindowsServicesConfiguration _windowsServicesConfiguration;

        [Export("ClipboardUpdates")]
        private ISubject<IEnumerable<string>> ClipboardUpdates { get; } = new Subject<IEnumerable<string>>();

        private IDisposable _clipboardMonitor;
        private readonly SynchronizationContext _uiSynchronizationContext;

        /// <summary>
        /// Importing constructor
        /// </summary>
        /// <param name="windowsServicesConfiguration">IWindowsServicesConfiguration</param>
        /// <param name="uiSynchronizationContext">SynchronizationContext</param>
        [ImportingConstructor]
        public WindowsServicesStartup(
            IWindowsServicesConfiguration windowsServicesConfiguration,
            [Import("ui", typeof(SynchronizationContext))]SynchronizationContext uiSynchronizationContext)
        {
            _windowsServicesConfiguration = windowsServicesConfiguration;
            _uiSynchronizationContext = uiSynchronizationContext;
        }
        /// <summary>
        /// Start will register all needed services
        /// </summary>
        public void Start()
        {
            if (!_windowsServicesConfiguration.AllowClipboardMonitoring)
            {
                return;
            }
            _clipboardMonitor = ClipboardMonitor.OnUpdate.SubscribeOn(_uiSynchronizationContext).Subscribe(clipboardContents =>
            {
                ClipboardUpdates.OnNext(clipboardContents.Formats);
            });
            Log.Info().WriteLine("Registered to clipboard updates.");
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
