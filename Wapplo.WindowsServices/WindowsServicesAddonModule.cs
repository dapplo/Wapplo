using Autofac;
using Dapplo.Addons;
using Microsoft.AspNet.SignalR.Hubs;
using Wapplo.WindowsServices.Hub;

namespace Wapplo.WindowsServices
{
    /// <summary>
    /// MAke sure all our types are registered
    /// </summary>
    public class WindowsServicesAddonModule : AddonModule
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<WindowsServicesHub>()
                .As<IHub>()
                .AsSelf()
                .SingleInstance();
            
            base.Load(builder);
        }
    }
}
