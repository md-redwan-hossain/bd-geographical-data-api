using Autofac;
using BdGeographicalData.Application;
using BdGeographicalData.Domain;
using BdGeographicalData.Persistence;

namespace BdGeographicalData.Infrastructure;

public class InfrastructureModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterModule(new PersistenceModule());

    builder
    .RegisterType<ApplicationUnitOfWork>()
    .As<IApplicationUnitOfWork>()
    .InstancePerLifetimeScope();

    builder
    .RegisterType<DivisionRepository>()
    .As<IDivisionRepository>()
    .InstancePerLifetimeScope();
  }

}
