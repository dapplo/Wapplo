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
	[Menu("contextmenu")]
	public sealed class TitleMenuItem : MenuItem
	{
	    private readonly IContextMenuTranslations _contextMenuTranslations;

	    /// <inheritdoc />
	    public TitleMenuItem(
	        IContextMenuTranslations contextMenuTranslations)
	    {
	        _contextMenuTranslations = contextMenuTranslations;
	    }

		/// <summary>
		/// Create the title of the context menu
		/// </summary>
		public override void Initialize()
		{
			Id = "A_Title";
            // automatically update the DisplayName
		    _contextMenuTranslations.CreateDisplayNameBinding(this, nameof(IContextMenuTranslations.Title));
			Style = MenuItemStyles.Title;

			Icon = new PackIconMaterial
			{
				Kind = PackIconMaterialKind.Web
			};
			this.ApplyIconForegroundColor(Brushes.DarkRed);
		}
	}
}