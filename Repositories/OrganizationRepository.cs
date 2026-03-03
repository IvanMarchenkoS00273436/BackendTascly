using BackendTascly.Data;
using BackendTascly.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTascly.Repositories
{
    public class OrganizationRepository(TasclyDbContext context) : IOrganizationsRepository
    {
        public async Task<Organization?> GetOrganization(Guid organizationId)
        {
            var org = await context.Organizations.Where(o => o.Id == organizationId)
                .Include(o => o.Members)
                .Include(o => o.Workspaces)
                    .ThenInclude(o => o.Projects)
                    .ThenInclude(p => p.TaskStatuses)
                .Include(o => o.Workspaces)
                    .ThenInclude(o => o.Projects)
                    .ThenInclude(p => p.Tasks)
                .FirstOrDefaultAsync();
            return org;
        }

        public async Task<bool> UpdateOrganization(Organization organization)
        {
            context.Organizations.Update(organization);
            bool success = await context.SaveChangesAsync() > 0;
            return success;
        }
    }
}
