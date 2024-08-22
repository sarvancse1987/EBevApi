namespace EBev.Application
{
    public interface IBlogService
    {
        Task<BlogVm> Add(BlogVm request, CancellationToken ct = default(CancellationToken));
        Task<bool> Update(int blogId, BlogVm request, CancellationToken ct = default);
        Task<BlogVm> Get(int blogId);
        Task<IEnumerable<BlogVm>> GetAll();
        Task<bool> Delete(int blogId);
    }
}
