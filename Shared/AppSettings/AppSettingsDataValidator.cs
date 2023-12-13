using FluentValidation;

namespace BdGeographicalData.Shared.AppSettings;

public class AppSettingsDataValidator : AbstractValidator<IAppSettingsData>
{
    public AppSettingsDataValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(appSettingsData => appSettingsData.DatabaseUrl)
            .NotNull()
            .NotEmpty();

        RuleFor(appSettingsData => appSettingsData.ResponseCacheDurationInSecond)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(1);
    }
}