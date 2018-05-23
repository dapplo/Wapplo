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

using System;
using System.Windows;
using Dapplo.Addons.Bootstrapper;
using Dapplo.CaliburnMicro.Dapp;
using Dapplo.Log;
using Dapplo.Log.Loggers;

#endregion

namespace Wapplo
{
    /// <summary>
    ///     This takes care or starting Wapplo
    /// </summary>
    public static class Startup
    {
        /// <summary>
        ///     Start Wapplo Dapplication
        /// </summary>
        [STAThread]
        public static void Main()
        {
#if DEBUG
            // Initialize a debug logger for Dapplo packages
            LogSettings.RegisterDefaultLogger<DebugLogger>(LogLevels.Verbose);
#endif
            var applicationConfig = ApplicationConfig.Create()
                .WithApplicationName("Wapplo")
                .WithMutex("BF63D6C4-5F1A-4D43-87C7-0607EB50D0D0")
                .WithAssemblyNames("Dapplo.Addons.Config", "Dapplo.Owin", "Dapplo.Signalr")
                .WithAssemblyPatterns("Wapplo*")
                .WithScanDirectories(
#if DEBUG
                @"..\..\..\Wapplo.ShareContext\bin\Debug",
                @"..\..\..\Wapplo.WindowsServices\bin\Debug"
#else
                @"..\..\..\Wapplo.ShareContext\bin\Release",
                @"..\..\..\Wapplo.WindowsServices\bin\DebugRelease"
#endif
                );

            var dapplication = new Dapplication(applicationConfig)
            {
                ShutdownMode = ShutdownMode.OnExplicitShutdown
            };

            dapplication.Run();
        }
    }
}