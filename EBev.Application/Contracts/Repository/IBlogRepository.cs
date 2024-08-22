namespace EBev.Application
{
    public interface IBlogRepository
    {
        Task<Blog> Add(Blog request, CancellationToken ct = default(CancellationToken));
        Task<Blog> Update(Blog request, CancellationToken ct = default);
        Task<Blog> Get(Expression<Func<Blog, bool>>? predicate = null);
        Task<IEnumerable<Blog>> GetAll(Expression<Func<Blog, bool>>? predicate = null);
    }
}
