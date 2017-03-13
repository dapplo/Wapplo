using Dapplo.Addons;
using Dapplo.Owin;

namespace Wapplo.Modules
{
	/// <summary>
	/// Helper to start/stop owin
	/// </summary>
	[StartupAction, ShutdownAction]
	public class OwinServerStartupShutdownAction : BaseOwinServerStartupShutdownAction
	{
	}
}
