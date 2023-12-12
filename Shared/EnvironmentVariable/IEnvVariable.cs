namespace BdGeographicalData.Shared.EnvironmentVariable;

public interface IEnvVariable
{
    public string? DatabaseUrl { get; init; }
    public int ResponseCacheDurationInSecond { get; init; }

    public int UseSecretsJson { get; init; }
    // UseSecretsJson can take either -1 or 1
    // -1 means false, 1 means true.
    // since boolean has the default value of false,
    // Integer is used instead of boolean to be more explicit.
}