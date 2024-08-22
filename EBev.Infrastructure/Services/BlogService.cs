namespace EBev.Infrastructure
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public BlogService(IBlogRepository blogRepository
            , ICurrentUserService currentUserService
            , IMapper mapper)
        {
            _blogRepository = blogRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        public async Task<BlogVm> Add(BlogVm request, CancellationToken ct = default)
        {
            request.IsActive = true;
            request.PersonId = 1;
            request.CreatedBy = 999;
            request.CreatedOn = DateTime.Now;
            Blog response = await _blogRepository.Add(_mapper.Map<Blog>(request), ct);
            return request;
        }

        public async Task<bool> Update(int blogId, BlogVm request, CancellationToken ct = default)
        {
            request.EditedBy = _currentUserService.MemberId;
            Blog response = await _blogRepository.Update(_mapper.Map<Blog>(request), ct);
            return true;
        }

        public async Task<BlogVm> Get(int blogId)
        {
            Blog response = await _blogRepository.Get(x => x.Id == blogId);
            return _mapper.Map<BlogVm>(response);
        }

        public async Task<IEnumerable<BlogVm>> GetAll()
        {
            IEnumerable<Blog> response = await _blogRepository.GetAll(x => x.IsActive);
            return _mapper.Map<List<BlogVm>>(response);
        }

        public async Task<bool> Delete(int blogId)
        {
            Blog blog = await _blogRepository.Get(x => x.Id == blogId);
            var response = await _blogRepository.Delete(blog);
            return response;
        }
    }
}
