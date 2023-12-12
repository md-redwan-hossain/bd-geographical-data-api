using FluentValidation;

namespace BdGeographicalData.Shared.EnvironmentVariable;

public class DevelopmentFactory(IConfiguration configuration) : IEnvVariableFactory
{
    private static IEnvVariable? _envVariable;

    public IEnvVariable CreateOrGet()
    {
        if (_envVariable is not null)
            return _envVariable;

        var envVars = new Development
        {
            DatabaseUrl = configuration.GetConnectionString("DATABASE_URL"),
            ResponseCacheDurationInSecond = configuration.GetValue<int>("RESPONSE_CACHE_DURATION_IN_SECOND"),
            UseSecretsJson = configuration.GetValue<int>("USE_SECRETS_JSON")
        };

        new EnvVariableValidator().ValidateAndThrow(envVars);
        _envVariable = envVars;

        return _envVariable;
    }
}