using Autofac;
using BdGeographicalData.Domain.Features;

namespace BdGeographicalData.Application
{
  public class ApplicationModule : Module
  {

    protected override void Load(ContainerBuilder builder)
    {
      builder
      .RegisterType<DivisionService>()
      .As<IDivisionService>()
      .InstancePerLifetimeScope();
    }

  }
}
