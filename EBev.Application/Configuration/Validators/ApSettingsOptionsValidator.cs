namespace EBev.Application
{
    public class ApSettingsOptionsValidator : IValidateOptions<ApSettingsOptions>
    {
        public ValidateOptionsResult Validate(string name, ApSettingsOptions options)
        {
            if (options != null && !string.IsNullOrEmpty(options.ConnectionString))
            {
                return ValidateOptionsResult.Fail($"{nameof(ApSettingsOptions.ConnectionString)} for {nameof(ApSettingsOptions)} must be provided.");
            }

            return ValidateOptionsResult.Success;
        }
    }
}
