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
using System.ComponentModel.Composition;
using System.Linq;
using System.Reactive.Subjects;
using AdaptiveCards;
using Dapplo.CaliburnMicro.Cards.ViewModels;
using Dapplo.CaliburnMicro.Toasts;
using Dapplo.Utils;
using Dapplo.Windows.Clipboard;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Wapplo.WindowsServices.Configuration;

namespace Wapplo.WindowsServices.Hub
{
    /// <summary>
    /// The Signal-R hub for making Windows services available
    /// </summary>
    public class WindowsServicesHub : Hub<IWindowsServicesClient>, IWindowsServicesServer
    {
        private static readonly IDictionary<string, IDisposable> ClipboardSubscriptions = new ConcurrentDictionary<string, IDisposable>();

        [Import]
        private IWindowsServicesConfiguration WindowsServicesConfiguration { get; set; }

        [Import]
        private ToastConductor ToastConductor { get; set; }

        [Import]
        private ISubject<ClipboardContents> ClipboardUpdates { get; set; }

        /// <inheritdoc />
        public void CopyToClipboard(string origin, string text, string format = "CF_UNICODETEXT")
        {
            using (ClipboardNative.Lock())
            {
                ClipboardNative.SetAsString(text, format);
            }
        }

        /// <inheritdoc />
        public void MonitorClipboard(bool enable)
        {
            if (!WindowsServicesConfiguration.AllowClipboardMonitoring)
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
                var subscription = ClipboardUpdates.Subscribe(contents =>
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
        public string GetClipboardContent(string format = "CF_UNICODETEXT")
        {
            using (ClipboardNative.Lock())
            {
                return ClipboardNative.GetAsString(format);
            }
        }

        /// <inheritdoc />
        public void ShowAdaptiveCard(AdaptiveCard adaptiveCard)
        {
            UiContext.RunOn(() => ToastConductor.ActivateItem(new AdaptiveCardViewModel(adaptiveCard)));
        }
    }
}
