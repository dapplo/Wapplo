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

using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Dapplo.CaliburnMicro.Extensions;
using Dapplo.CaliburnMicro.Menu;
using MahApps.Metro.IconPacks;
using Wapplo.Languages;

#endregion

namespace Wapplo.Ui
{
	/// <summary>
	///     This will add an extry for the exit to the context menu
	/// </summary>
	[Export("contextmenu", typeof(IMenuItem))]
	public sealed class ExitMenuItem : ClickableMenuItem
	{
		[Import]
		private IContextMenuTranslations ContextMenuTranslations { get; set; }

		/// <summary>
		/// Handle the click on the exit item of the context menu
		/// </summary>
		/// <param name="clickedItem"></param>
		public override void Click(IMenuItem clickedItem)
		{
			Application.Current.Shutdown();
		}

		/// <summary>
		/// Initialize the exit menu item
		/// </summary>
		public override void Initialize()
		{
			Id = "Z_Exit";
			// automatically update the DisplayName
			ContextMenuTranslations.CreateDisplayNameBinding(this, nameof(IContextMenuTranslations.Exit));

			Icon = new PackIconMaterial
			{
				Kind = PackIconMaterialKind.Close
			};
			this.ApplyIconForegroundColor(Brushes.DarkRed);
		}
	}
}