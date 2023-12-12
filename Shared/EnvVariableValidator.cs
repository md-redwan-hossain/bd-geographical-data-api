using FluentValidation;

namespace BdGeographicalData.Shared;

public class EnvVariableValidator : AbstractValidator<EnvVariable>
{
    public EnvVariableValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(envVariable => envVariable.DatabaseUrl)
            .NotNull()
            .NotEmpty();

        RuleFor(envVariable => envVariable.UseSecretsJson)
            .NotNull()
            .NotEmpty()
            .InclusiveBetween(-1, 1);

        RuleFor(envVariable => envVariable.ResponseCacheDurationInSecond)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
    }
}