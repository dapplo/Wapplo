using Dapplo.Ini;
using System.ComponentModel;

namespace Wapplo.WindowsServices.Configuration
{
	/// <summary>
	/// Configuration for the windows services
	/// </summary>
	[IniSection("WindowsServices")]
	[Description("The configuration for the windows services module")]
	public interface IWindowsServicesConfiguration : IIniSection
	{
		/// <summary>
		/// Specify if web applications are allowed to monitor the clipboard, default is false
		/// </summary>
		[DefaultValue(false), Description("Specify if web applications are allowed to monitor the clipboard")]
		bool AllowClipboardMonitoring { get; set; }
	}
}
