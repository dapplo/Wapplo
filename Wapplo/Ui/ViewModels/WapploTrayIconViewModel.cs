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
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;
using Dapplo.CaliburnMicro.Extensions;
using Dapplo.CaliburnMicro.Menu;
using Dapplo.CaliburnMicro.NotifyIconWpf;
using Dapplo.CaliburnMicro.NotifyIconWpf.ViewModels;
using MahApps.Metro.IconPacks;
using Wapplo.Languages;

#endregion

namespace Wapplo.Ui.ViewModels
{
	/// <summary>
	///     Implementation of the system-tray icon
	/// </summary>
	[Export(typeof(ITrayIconViewModel))]
	public class WapploTrayIconViewModel : TrayIconViewModel
	{
		[ImportMany("contextmenu", typeof(IMenuItem))]
		private IEnumerable<Lazy<IMenuItem>> ContextMenuItems { get; set; }

		[Import]
		private IContextMenuTranslations ContextMenuTranslations { get; set; }

		[Import]
		private IEventAggregator EventAggregator { get; set; }

		/// <inheritdoc />
		protected override void OnActivate()
		{
			base.OnActivate();

			// Set the title of the icon (the ToolTipText) to our IContextMenuTranslations.Title
			ContextMenuTranslations.CreateDisplayNameBinding(this, nameof(IContextMenuTranslations.Title));

			var items = new List<IMenuItem>();

			// Lazy values
			items.AddRange(ContextMenuItems.Select(lazy => lazy.Value));

			items.Add(new MenuItem
			{
				Style = MenuItemStyles.Separator,
				Id = "Y_Separator"
			});
			ConfigureMenuItems(items);

			// Make sure the margin is set, do this AFTER the icon are set
			items.ApplyIconMargin(new Thickness(2, 2, 2, 2));

			Icon = new PackIconMaterial
			{
				Kind = PackIconMaterialKind.Web,
				Background = Brushes.White,
				Foreground = Brushes.Black
			};
			Show();
			EventAggregator.Subscribe(this);
		}
	}
}