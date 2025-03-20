namespace Bookly.APIs.DTOs
{
    public class RoleDto : BaseRoleDto
    {
        public IEnumerable<string> Roles { get; set; }
    }
}
