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
using Dapplo.Log;
using MahApps.Metro.IconPacks;
using Wapplo.Languages;

namespace Wapplo.Ui.ViewModels
{
	/// <summary>
	/// Implementation of the system-tray icon
	/// </summary>
	[Export(typeof(ITrayIconViewModel))]
	public class WapploTrayIconViewModel : TrayIconViewModel
	{
		private static readonly LogSource Log = new LogSource();

		[ImportMany("contextmenu", typeof(IMenuItem))]
		private IEnumerable<Lazy<IMenuItem>> ContextMenuItems { get; set; }

		[Import]
		private IEventAggregator EventAggregator { get; set; }

		[Import]
		public IWindowManager WindowManager { get; set; }

		[Import]
		private IContextMenuTranslations ContextMenuTranslations { get; set; }

		public void Handle(string message)
		{
			var trayIcon = TrayIconManager.GetTrayIconFor(this);
			trayIcon.ShowBalloonTip("Event", message);
		}

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
				Foreground = Brushes.Black,
			};
			Show();
			EventAggregator.Subscribe(this);
		}

		public override void Click()
		{
			Log.Debug().WriteLine("Do something?");
		}
	}
}
