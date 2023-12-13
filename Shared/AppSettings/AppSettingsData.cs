namespace BdGeographicalData.Shared.AppSettings;

public class AppSettingsData : IAppSettingsData
{
    public string? DatabaseUrl { get; init; }
    public int ResponseCacheDurationInSecond { get; init; }
}