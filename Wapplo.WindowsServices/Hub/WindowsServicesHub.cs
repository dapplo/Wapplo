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
            using (ClipboardNative.Lock())
            {
                ClipboardNative.SetAsUnicodeString(text);
            }
        }

        /// <inheritdoc />
        public void MonitorClipboard(bool enable)
        {
            if (!_windowsServicesConfiguration.AllowClipboardMonitoring)
            {
                throw new NotSupportedException();
            }

            if (enable)
            {
                if (ClipboardSubscriptions.ContainsKey(Context.ConnectionId))
                {
                    // Do nothing, already subscribed
                    return;
                }
                // Create a subscription, which will inform of clipboard updates
                var subscription = _clipboardUpdates.ClipboardUpdates.Subscribe(contents =>
                {
                    Clients.Client(Context.ConnectionId).ClipboardChanged(contents);
                });
                ClipboardSubscriptions[Context.ConnectionId] = subscription;
            }
            else if (ClipboardSubscriptions.ContainsKey(Context.ConnectionId))
            {
                ClipboardSubscriptions[Context.ConnectionId].Dispose();
            }

        }

        /// <inheritdoc />
        public string GetClipboardString()
        {
            using (ClipboardNative.Lock())
            {
                return ClipboardNative.GetAsUnicodeString();
            }
        }

        /// <inheritdoc />
        public void ShowAdaptiveCard(AdaptiveCard adaptiveCard)
        {
            UiContext.RunOn(() => _toastConductor.ActivateItem(new AdaptiveCardViewModel(adaptiveCard)));
        }
    }
}
