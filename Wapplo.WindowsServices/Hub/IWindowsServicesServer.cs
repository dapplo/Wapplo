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

using AdaptiveCards;

namespace Wapplo.WindowsServices.Hub
{
    /// <summary>
    /// Windows services made available to clients
    /// </summary>
    public interface IWindowsServicesServer
    {
        /// <summary>
        /// Copy text to the clipboard
        /// </summary>
        /// <param name="origin">Where does the clipboard content come from</param>
        /// <param name="text">String to place onto the clipboard</param>
        /// <param name="format">Format to paste with</param>
        void CopyToClipboard(string origin, string text, string format = "CF_UNICODETEXT");

        /// <summary>
        /// Enable or disable clipboard monitoring for the client
        /// </summary>
        /// <param name="enable">bool to enable / disable clipboard monitoring for the client</param>
        void MonitorClipboard(bool enable);

        /// <summary>
        /// Get the clipboard contents for the specified content
        /// </summary>
        /// <param name="format">Clipboard format for the content to retrieve</param>
        string GetClipboardContent(string format = "CF_UNICODETEXT");


        /// <summary>
        /// Show an adaptive card
        /// </summary>
        /// <param name="toast"></param>
        void ShowAdaptiveCard(AdaptiveCard toast);
    }
}
