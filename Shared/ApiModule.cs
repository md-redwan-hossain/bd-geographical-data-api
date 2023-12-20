using Autofac;
using BdGeographicalData.Api.District;
using BdGeographicalData.Api.Division;
using BdGeographicalData.Api.SubDistrict;

namespace BdGeographicalData.Shared;

public class ApiModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<ResponseCacheConfigMiddleware>()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<DivisionService>()
            .As<IDivisionService>()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<DistrictService>()
            .As<IDistrictService>()
            .InstancePerLifetimeScope();

        builder
            .RegisterType<SubDistrictService>()
            .As<ISubDistrictService>()
            .InstancePerLifetimeScope();
    }
}