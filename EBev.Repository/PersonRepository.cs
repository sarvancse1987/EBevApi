namespace EBev
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IUnitOfWorkWrite _unitOfWork = null;
        private readonly EBevDbContext _dbcontext;
        public PersonRepository(IUnitOfWorkWrite unitOfWork, EBevDbContext dbcontext)
        {
            _unitOfWork = unitOfWork;
            _dbcontext = dbcontext;
        }

        public async Task<Person> Add(Person request, CancellationToken ct = default)
        {
            _unitOfWork.GetRepository<Person>().Insert(request);
            await _unitOfWork.SaveChangesAsync();
            return request;
        }

        public async Task<Person> Update(Person request, CancellationToken ct = default)
        {
            _unitOfWork.GetRepository<Person>().Update(request);
            await _unitOfWork.SaveChangesAsync();
            return request;
        }

        public async Task<Person> Get(Expression<Func<Person, bool>>? predicate = null)
        {
            return _unitOfWork.GetRepository<Person>().GetFirstOrDefault(skipBaseProperties: true, predicate: predicate);
        }

        public async Task<IEnumerable<Person>> GetAll(Expression<Func<Person, bool>>? predicate = null)
        {
            return _unitOfWork.GetRepository<Person>().GetAll(skipBaseProperties: true, predicate: predicate);
        }
    }
}
