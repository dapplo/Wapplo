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
using Dapplo.Addons;
using Wapplo.WindowsServices.Configuration;
using System.Reactive.Linq;
using System.Threading;
using Autofac.Features.AttributeFilters;
using Dapplo.CaliburnMicro;
using Dapplo.Log;
using Dapplo.Windows.Clipboard;

namespace Wapplo.WindowsServices
{
    /// <summary>
    /// Make it possible to subscribe to events
    /// </summary>
    [Service(nameof(WindowsServicesStartup), nameof(CaliburnServices.CaliburnMicroBootstrapper), nameof(CaliburnServices.IniSectionService), TaskSchedulerName = "ui")]
    public class WindowsServicesStartup : IStartup, IShutdown
    {
        private static readonly LogSource Log = new LogSource();
        private readonly ClipboardSubjectHolder _clipboardSubjectHolder;
        private readonly IWindowsServicesConfiguration _windowsServicesConfiguration;
        private IDisposable _clipboardMonitor;
        private readonly SynchronizationContext _uiSynchronizationContext;

        /// <summary>
        /// Importing constructor
        /// </summary>
        /// <param name="clipboardSubjectHolder">ClipboardSubjectHolder</param>
        /// <param name="windowsServicesConfiguration">IWindowsServicesConfiguration</param>
        /// <param name="uiSynchronizationContext">SynchronizationContext</param>
        public WindowsServicesStartup(
            ClipboardSubjectHolder clipboardSubjectHolder,
            IWindowsServicesConfiguration windowsServicesConfiguration,
            [KeyFilter("ui")]SynchronizationContext uiSynchronizationContext)
        {
            _clipboardSubjectHolder = clipboardSubjectHolder;
            _windowsServicesConfiguration = windowsServicesConfiguration;
            _uiSynchronizationContext = uiSynchronizationContext;
        }
        /// <summary>
        /// Start will register all needed services
        /// </summary>
        public void Startup()
        {
            if (!_windowsServicesConfiguration.AllowClipboardMonitoring)
            {
                return;
            }
            _clipboardMonitor = ClipboardNative.OnUpdate.SubscribeOn(_uiSynchronizationContext).Subscribe(clipboardContents =>
            {
                _clipboardSubjectHolder.ClipboardUpdates.OnNext(clipboardContents);
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
