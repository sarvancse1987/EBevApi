namespace EBev.Repository
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services
                .AddSingleton<IBlogRepository, BlogRepository>()
                .AddSingleton<IPersonRepository, PersonRepository>();

            return services;
        }
    }
}
