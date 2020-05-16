using Autofac;
using Microsoft.Extensions.Logging;
using Project.Service.Models;
using Project.Service;

namespace ProjectMono.Models
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // The generic ILogger<TCategoryName> service was added to the ServiceCollection by ASP.NET Core.
            // It was then registered with Autofac using the Populate method. All of this starts
            // with the services.AddAutofac() that happens in Program and registers Autofac
            // as the service provider.
            builder.Register(c => new VMakeRepository(c.Resolve<ILogger<VMakeRepository>>()))
                .As<IVehicleMakeRepository>()
                .InstancePerLifetimeScope();

            //builder.Register(c => new VModelRepository(c.Resolve<ILogger<IVehicleModelRepository>>()))
            //  .As<IVehicleModelRepository>()
            //  .InstancePerLifetimeScope();
        }
    }
}
