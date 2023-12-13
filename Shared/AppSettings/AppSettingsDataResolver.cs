using FluentValidation;

namespace BdGeographicalData.Shared.AppSettings;

public class AppSettingsDataResolver(IConfiguration configuration) : IAppSettingsDataResolver
{
    private static IAppSettingsData? _appSettingsData;

    public IAppSettingsData Resolve()
    {
        if (_appSettingsData is not null)
            return _appSettingsData;

        var appSettingsData = new AppSettingsData
        {
            DatabaseUrl = configuration.GetConnectionString("DATABASE_URL"),
            ResponseCacheDurationInSecond = configuration.GetValue<int>("RESPONSE_CACHE_DURATION_IN_SECOND")
        };

        new AppSettingsDataValidator().ValidateAndThrow(appSettingsData);
        _appSettingsData = appSettingsData;

        return _appSettingsData;
    }
}