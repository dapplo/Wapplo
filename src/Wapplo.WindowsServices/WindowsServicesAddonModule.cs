using Autofac;
using Autofac.Features.AttributeFilters;
using Dapplo.Addons;
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
                .AsSelf()
                .SingleInstance();

            builder
                .RegisterType<WindowsServicesStartup>()
                .As<IService>()
                .WithAttributeFiltering()
                .SingleInstance();

            builder
                .RegisterType<ClipboardSubjectHolder>()
                .AsSelf()
                .SingleInstance();

            base.Load(builder);
        }
    }
}
