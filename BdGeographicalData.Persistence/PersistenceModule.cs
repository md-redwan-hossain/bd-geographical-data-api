using Autofac;

namespace BdGeographicalData.Persistence;

public class PersistenceModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder
    .RegisterType<ApplicationDbContext>()
    .AsSelf()
    .InstancePerLifetimeScope();
  }
}
