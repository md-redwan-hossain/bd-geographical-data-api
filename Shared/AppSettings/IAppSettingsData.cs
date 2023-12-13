namespace BdGeographicalData.Shared.AppSettings;

public interface IAppSettingsData
{
    public string? DatabaseUrl { get; init; }
    public int ResponseCacheDurationInSecond { get; init; }
}