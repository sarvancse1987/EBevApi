namespace EBev.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services
                .AddSingleton<IBlogService, BlogService>()
                .AddSingleton<IPersonService, PersonService>()
                .AddSingleton<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
