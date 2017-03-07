using System.ComponentModel;
using Dapplo.Ini;
using Dapplo.Owin;

namespace Wapplo.Modules
{
	/// <summary>
	/// The configuration for the web-server (owin)
	/// </summary>
	[IniSection("Webserver")]
	[Description("The configuration for the web-server (owin)")]
	public interface IWebserverConfiguration : IIniSection, IOwinConfiguration
	{
	}
}
