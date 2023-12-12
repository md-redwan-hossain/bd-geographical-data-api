namespace BdGeographicalData.Shared.EnvironmentVariable;

public class Production : IEnvVariable
{
    public string? DatabaseUrl { get; init; }
    public int ResponseCacheDurationInSecond { get; init; }
}