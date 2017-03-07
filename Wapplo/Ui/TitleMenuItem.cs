using System.ComponentModel.Composition;
using System.Windows.Media;
using Dapplo.CaliburnMicro.Extensions;
using Dapplo.CaliburnMicro.Menu;
using MahApps.Metro.IconPacks;
using Wapplo.Languages;

namespace Wapplo.Ui
{
	/// <summary>
	/// This will add an extry for the exit to the context menu
	/// </summary>
	[Export("contextmenu", typeof(IMenuItem))]
	public sealed class TitleMenuItem : MenuItem
	{
		[Import]
		private IContextMenuTranslations ContextMenuTranslations { get; set; }

		public override void Initialize()
		{
			Id = "A_Title";
			// automatically update the DisplayName
			ContextMenuTranslations.CreateDisplayNameBinding(this, nameof(IContextMenuTranslations.Title));
			Style = MenuItemStyles.Title;

			Icon = new PackIconMaterial
			{
				Kind = PackIconMaterialKind.Exclamation
			};
			this.ApplyIconForegroundColor(Brushes.DarkRed);
		}
	}
}
