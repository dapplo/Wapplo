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
using System.Collections.Concurrent;
using System.Collections.Generic;
using AdaptiveCards;
using Dapplo.CaliburnMicro.Cards.ViewModels;
using Dapplo.CaliburnMicro.Toasts;
using Dapplo.Log;
using Dapplo.Utils;
using Dapplo.Windows.Clipboard;
using Microsoft.AspNet.SignalR;
using Wapplo.WindowsServices.Configuration;

namespace Wapplo.WindowsServices.Hub
{
    /// <summary>
    /// The Signal-R hub for making Windows services available
    /// </summary>
    public class WindowsServicesHub : Hub<IWindowsServicesClient>, IWindowsServicesServer
    {
        private static readonly LogSource Log = new LogSource();
        private static readonly IDictionary<string, IDisposable> ClipboardSubscriptions = new ConcurrentDictionary<string, IDisposable>();

        private readonly IWindowsServicesConfiguration _windowsServicesConfiguration;
        private readonly ToastConductor _toastConductor;
        private readonly ClipboardSubjectHolder _clipboardUpdates;

        /// <summary>
        /// Constructor for the Hub
        /// </summary>
        /// <param name="windowsServicesConfiguration">IWindowsServicesConfiguration</param>
        /// <param name="toastConductor">ToastConductor</param>
        /// <param name="clipboardUpdates">ISubject of ClipboardUpdateInformation</param>
        public WindowsServicesHub(
            IWindowsServicesConfiguration windowsServicesConfiguration,
            ToastConductor toastConductor,
            ClipboardSubjectHolder clipboardUpdates
            )
        {
            _windowsServicesConfiguration = windowsServicesConfiguration;
            _toastConductor = toastConductor;
            _clipboardUpdates = clipboardUpdates;
        }

        /// <inheritdoc />
        public void CopyToClipboard(string origin, string text)
        {
            using (var clipboardAccessToken = ClipboardNative.Access())
            {
                clipboardAccessToken.SetAsUnicodeString(text);
            }
        }

        /// <inheritdoc />
        public void MonitorClipboard(bool enable)
        {
            if (!_windowsServicesConfiguration.AllowClipboardMonitoring)
            {
                throw new NotSupportedException();
            }

            var connectionId = Context.ConnectionId;

            if (enable)
            {
                if (ClipboardSubscriptions.ContainsKey(connectionId))
                {
                    Log.Verbose().WriteLine("Skipping clipboard subscription for {0}", connectionId);
                    // Do nothing, already subscribed
                    return;
                }
                Log.Verbose().WriteLine("Registering clipboard subscription for {0}", connectionId);
                // Create a subscription, which will inform of clipboard updates
                ClipboardSubscriptions[connectionId] = _clipboardUpdates.ClipboardUpdates.Subscribe(contents =>
                {
                    Log.Verbose().WriteLine("Sending clipboard information {0} to {1}", contents.Id, connectionId);
                    Clients.Client(connectionId).ClipboardChanged(contents);
                });
            }
            else if (ClipboardSubscriptions.ContainsKey(connectionId))
            {
                Log.Verbose().WriteLine("Removing clipboard subscription for {0}", connectionId);
                ClipboardSubscriptions[connectionId].Dispose();
            }

        }

        /// <inheritdoc />
        public string GetClipboardString()
        {
            using (var clipboardAccessToken = ClipboardNative.Access())
            {
                return clipboardAccessToken.GetAsUnicodeString();
            }
        }

        /// <inheritdoc />
        public void ShowAdaptiveCard(AdaptiveCard adaptiveCard)
        {
            UiContext.RunOn(() => _toastConductor.ActivateItem(new AdaptiveCardViewModel(adaptiveCard)));
        }
    }
}
