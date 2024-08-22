namespace EBev.Application
{
    public interface IPersonService
    {
        Task<PersonVm> Add(PersonVm request, CancellationToken ct = default(CancellationToken));
        Task<bool> Update(int blogId, PersonVm request, CancellationToken ct = default);
        Task<PersonVm> Get(int blogId);
        Task<IEnumerable<PersonVm>> GetAll();
    }
}
