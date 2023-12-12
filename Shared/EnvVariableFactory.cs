using FluentValidation;

namespace BdGeographicalData.Shared;

public class EnvVariableFactory(IConfiguration configuration) : IEnvVariableFactory
{
    private static EnvVariable? _envVariable;

    public EnvVariable CreateOrGet()
    {
        if (_envVariable is not null)
            return _envVariable;

        var envVars = new EnvVariable
        {
            DatabaseUrl = configuration.GetConnectionString("DATABASE_URL"),
            CacheDurationInSecond = configuration.GetValue<int>("CACHE_DURATION_IN_SECOND"),
            UseSecretsJson = configuration.GetValue<int>("USE_SECRETS_JSON")
        };

        new EnvVariableValidator().ValidateAndThrow(envVars);
        _envVariable = envVars;

        return _envVariable;
    }
}