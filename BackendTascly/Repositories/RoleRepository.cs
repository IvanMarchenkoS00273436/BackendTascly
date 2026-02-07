using BackendTascly.Data;
using BackendTascly.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTascly.Repositories
{
    public class RoleRepository(TasclyDbContext context): IRoleRepository
    {
        public async Task<Role> GetAdminRoleAsync()
        {
            return await context.Roles.FirstAsync(r => r.Name == "Admin");
        }

        public async Task<Role> GetFullAccessRoleAsync()
        {
            return await context.Roles.FirstAsync(r => r.Name == "Full-access");
        }

        public async Task<Role> GetLimitedAccessRoleAsync()
        {
            return await context.Roles.FirstAsync(r => r.Name == "Limited-access");
        }
    }
}
