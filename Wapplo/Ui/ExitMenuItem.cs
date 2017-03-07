using System.ComponentModel.Composition;
using System.Windows;
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
	public sealed class ExitMenuItem : MenuItem
	{
		[Import]
		private IContextMenuTranslations ContextMenuTranslations { get; set; }

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

		public override void Click(IMenuItem clickedItem)
		{
			Application.Current.Shutdown();
		}
	}
}
