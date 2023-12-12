using FluentValidation;

namespace BdGeographicalData.Shared.EnvironmentVariable;

public class EnvVariableValidator : AbstractValidator<IEnvVariable>
{
    public EnvVariableValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(envVariable => envVariable.DatabaseUrl)
            .NotNull()
            .NotEmpty();

        RuleFor(envVariable => envVariable.ResponseCacheDurationInSecond)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
    }
}