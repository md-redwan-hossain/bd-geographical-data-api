using FluentValidation;

namespace BdRegionalData.Shared;

public class EnvVariableValidator : AbstractValidator<EnvVariable>
{
    public EnvVariableValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(envVariable => envVariable.DatabaseUrl)
            .NotNull()
            .NotEmpty();
    }
}