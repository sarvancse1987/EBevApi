namespace EBev.Application
{
    public interface ICurrentUserService
    {
        int MemberId { get; }
        string EmployeeId { get; }
        int UserRole { get; }
        string MemberName { get; }
        string[] Roles { get; }
    }
}
