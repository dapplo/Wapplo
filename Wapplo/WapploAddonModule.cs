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

using Autofac;
using Autofac.Features.AttributeFilters;
using Dapplo.Addons;
using Dapplo.CaliburnMicro.Menu;
using Dapplo.CaliburnMicro.NotifyIconWpf;
using Dapplo.Owin;
using Wapplo.Modules;
using Wapplo.Ui;
using Wapplo.Ui.ViewModels;

namespace Wapplo
{
    /// <summary>
    /// MAke sure all our types are registered
    /// </summary>
    public class WapploAddonModule : AddonModule
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ArgumentsStartup>()
                .As<IService>()
                .SingleInstance();
            builder
                .RegisterType<ConfigureOwinFileServer>()
                .As<IOwinModule>()
                .SingleInstance();
            builder
                .RegisterType<OwinService>()
                .As<IService>()
                .SingleInstance();


            builder
                .RegisterType<WapploTrayIconViewModel>()
                .As<ITrayIconViewModel>()
                .WithAttributeFiltering()
                .SingleInstance();

            builder
                .RegisterType<ExitMenuItem>()
                .As<IMenuItem>()
                .SingleInstance();
            builder
                .RegisterType<TitleMenuItem>()
                .As<IMenuItem>()
                .SingleInstance();
            base.Load(builder);
        }
    }
}
