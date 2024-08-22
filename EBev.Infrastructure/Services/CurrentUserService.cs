namespace EBev.Infrastructure
{
    public class CurrentUserService : ICurrentUserService
    {
        private IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            if (_httpContextAccessor.HttpContext.Request.Headers["memberId"] != "undefined")
            {
                MemberId = Convert.ToInt32(_httpContextAccessor.HttpContext.Request.Headers["memberId"]);
                EmployeeId = Convert.ToString(_httpContextAccessor.HttpContext.Request.Headers["employeeId"]);
                MemberName = Convert.ToString(httpContextAccessor.HttpContext.Request.Headers["memberName"]);
                UserRole = Convert.ToInt32(httpContextAccessor.HttpContext.Request.Headers["userRole"]);
                Roles = httpContextAccessor.HttpContext?.User?.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToArray();
            }
        }

        public int MemberId { get; }
        public string MemberName { get; }
        public string EmployeeId { get; }
        public int UserRole { get; }
        public string[] Roles { get; }
    }
}