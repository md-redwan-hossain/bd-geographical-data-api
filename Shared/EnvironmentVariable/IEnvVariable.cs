namespace BdGeographicalData.Shared.EnvironmentVariable;

public interface IEnvVariable
{
    public string? DatabaseUrl { get; init; }
    public int ResponseCacheDurationInSecond { get; init; }
}