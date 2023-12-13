namespace BdGeographicalData.Shared.AppSettings;

public interface IAppSettingsDataResolver
{
    public IAppSettingsData Resolve();
}