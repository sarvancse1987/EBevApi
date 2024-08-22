namespace EBev.Application
{
    public interface IPersonRepository
    {
        Task<Person> Add(Person request, CancellationToken ct = default(CancellationToken));
        Task<Person> Update(Person request, CancellationToken ct = default);
        Task<Person> Get(Expression<Func<Person, bool>>? predicate = null);
        Task<IEnumerable<Person>> GetAll(Expression<Func<Person, bool>>? predicate = null);
    }
}
