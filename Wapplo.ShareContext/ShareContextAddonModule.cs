using Autofac;
using Dapplo.Addons;
using Microsoft.AspNet.SignalR.Hubs;
using Wapplo.ShareContext.Hub;

namespace Wapplo.ShareContext
{
    /// <summary>
    /// MAke sure all our types are registered
    /// </summary>
    public class ShareContextAddonModule : AddonModule
    {
        /// <inheritdoc />
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ShareContextHub>()
                .As<IHub>()
                .AsSelf()
                .SingleInstance();
            
            base.Load(builder);
        }
    }
}
