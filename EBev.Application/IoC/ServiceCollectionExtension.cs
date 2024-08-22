namespace EBev.Application
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddConfigurationOptions(this IServiceCollection services)
        {
            services
               .AddSingleton<IValidateOptions<ApSettingsOptions>, ApSettingsOptionsValidator>()
               .AddOptions<ApSettingsOptions>()
               .Configure<IConfiguration>((settings, configuration) =>
               {
                   configuration.GetSection(nameof(ApSettingsOptions)).Bind(settings);
               });

         

            return services;
        }
    }
}
