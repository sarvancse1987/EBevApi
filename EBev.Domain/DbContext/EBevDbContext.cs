namespace EBev.Domain
{
    public class EBevDbContext : DbContext
    {
        public EBevDbContext(DbContextOptions<EBevDbContext> options)
                : base(options)
        { }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Person> Person { get; set; }

    }
}
