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

using System.ComponentModel;
using Dapplo.Language;

#endregion

namespace Wapplo.Languages
{
	/// <summary>
	/// Translations for the context menu
	/// </summary>
	[Language("ContextMenu")]
	public interface IContextMenuTranslations : ILanguage, INotifyPropertyChanged
	{
		/// <summary>
		/// The translation for the context menu's configure item
		/// </summary>
		string Configure { get; }
		/// <summary>
		/// The translation for the context menu's exit item
		/// </summary>
		string Exit { get; }
		/// <summary>
		/// The translation for the context menu's title
		/// </summary>
		string Title { get; }
	}
}