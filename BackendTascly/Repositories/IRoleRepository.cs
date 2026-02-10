using BackendTascly.Entities;

namespace BackendTascly.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> GetAdminRoleAsync();
        Task<Role> GetFullAccessRoleAsync();
        Task<Role> GetLimitedAccessRoleAsync();
        Task<Role?> FindRoleByName(string roleName);
    }
}
