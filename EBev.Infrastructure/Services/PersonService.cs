namespace EBev.Infrastructure
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public PersonService(IPersonRepository personRepository
            , ICurrentUserService currentUserService
            , IMapper mapper)
        {
            _personRepository = personRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        public async Task<PersonVm> Add(PersonVm request, CancellationToken ct = default)
        {
            request.IsActive = true;
            request.CreatedBy = _currentUserService.MemberId;
            Person response = await _personRepository.Add(_mapper.Map<Person>(request), ct);
            return request;
        }

        public async Task<bool> Update(int blogId, PersonVm request, CancellationToken ct = default)
        {
            Person person = await _personRepository.Get(x => x.Id == blogId);
            person.ShortDescription = request.ShortDescription;
            person.Description = request.Description;
            person.ImageUrl = request.ImageUrl;
            person.EditedBy = _currentUserService.MemberId;
            Person response = await _personRepository.Update(person, ct);
            return true;
        }

        public async Task<PersonVm> Get(int blogId)
        {
            Person response = await _personRepository.Get(x => x.Id == blogId);
            return _mapper.Map<PersonVm>(response);
        }

        public async Task<IEnumerable<PersonVm>> GetAll()
        {
            IEnumerable<Person> response = await _personRepository.GetAll(x => x.IsActive);
            return _mapper.Map<List<PersonVm>>(response);
        }
    }
}
