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
            .InclusiveBetween(-1, 1);
    }
}