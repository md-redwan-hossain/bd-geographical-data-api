namespace BdGeographicalData.Shared;

public class EnvVariable
{
    public string? DatabaseUrl { get; init; }

    // -1 means false, 1 means true.
    // since boolean has the default value of false,
    // Integer is used instead of boolean to be more explicit.
    public int UseSecretsJson { get; init; }
}