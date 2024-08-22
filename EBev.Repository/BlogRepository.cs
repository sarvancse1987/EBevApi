namespace EBev
{
    public class BlogRepository : IBlogRepository
    {
        private readonly IUnitOfWorkWrite _unitOfWork = null;
        private readonly EBevDbContext _dbcontext;
        public BlogRepository(IUnitOfWorkWrite unitOfWork, EBevDbContext dbcontext)
        {
            _unitOfWork = unitOfWork;
            _dbcontext = dbcontext;
        }

        public async Task<Blog> Add(Blog request, CancellationToken ct = default)
        {
            _unitOfWork.GetRepository<Blog>().Insert(request);
            await _unitOfWork.SaveChangesAsync();
            return request;
        }

        public async Task<Blog> Update(Blog request, CancellationToken ct = default)
        {
            _unitOfWork.GetRepository<Blog>().Update(request);
            await _unitOfWork.SaveChangesAsync();
            return request;
        }

        public async Task<Blog> Get(Expression<Func<Blog, bool>>? predicate = null)
        {
            return _unitOfWork.GetRepository<Blog>().GetFirstOrDefault(skipBaseProperties: true, predicate: predicate);
        }

        public async Task<IEnumerable<Blog>> GetAll(Expression<Func<Blog, bool>>? predicate = null)
        {
            return _unitOfWork.GetRepository<Blog>().GetAll(skipBaseProperties: true, predicate: predicate);
        }
    }
}
