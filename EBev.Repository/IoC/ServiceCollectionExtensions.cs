namespace EBev.Repository
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services
                .AddTransient<IBlogRepository, BlogRepository>()
                .AddTransient<IPersonRepository, PersonRepository>();

            return services;
        }
    }
}
