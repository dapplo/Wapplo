using System.ComponentModel;
using Dapplo.Ini;

namespace Wapplo.ShareContext.Configuration
{
	/// <summary>
	/// Configuration for the share context
	/// </summary>
	[IniSection("ShareContext")]
	[Description("The configuration for the share context module")]
	public interface IShareContextConfiguration : IIniSection
	{
		/// <summary>
		/// Specify if web applications are allowed to share a context, default is true
		/// </summary>
		[DefaultValue(true), Description("Specify if web applications are allowed to share a context")]
		bool AllowShareContext { get; set; }
	}
}
