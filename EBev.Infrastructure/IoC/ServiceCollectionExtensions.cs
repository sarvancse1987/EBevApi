namespace EBev.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services
                .AddTransient<IBlogService, BlogService>()
                .AddTransient<IPersonService, PersonService>()
                .AddTransient<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
