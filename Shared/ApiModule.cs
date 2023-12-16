using Autofac;
using BdGeographicalData.Api.District;
using BdGeographicalData.Api.Division;
using BdGeographicalData.Api.SubDistrict;
using BdGeographicalData.Shared.AppSettings;

namespace BdGeographicalData.Shared;

public class ApiModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<AppSettingsDataResolver>()
            .As<IAppSettingsDataResolver>()
            .SingleInstance();

        builder
            .RegisterType<DivisionService>()
            .As<IDivisionService>()
            .InstancePerRequest();

        builder
            .RegisterType<DistrictService>()
            .As<IDistrictService>()
            .InstancePerRequest();

        builder
            .RegisterType<SubDistrictService>()
            .As<ISubDistrictService>()
            .InstancePerRequest();
    }
}