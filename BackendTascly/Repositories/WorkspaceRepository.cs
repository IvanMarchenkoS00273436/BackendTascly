using AutoMapper.Execution;
using BackendTascly.Data;
using BackendTascly.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTascly.Repositories
{
    public class WorkspaceRepository(TasclyDbContext context): IWorkspaceRepository
    {
        public async Task<bool> AddWorkspaceAsync(Workspace workspace)
        {
            if(workspace is null) return false;

            context.Workspaces.Add(workspace);

            var affected = await context.SaveChangesAsync();
            return affected > 0; // >0 means at least one entity row was written
        }
        public async Task<List<Workspace>> GetAllWorkspacesAsync(Guid organizationId)
        {
            var workspaces = await context.Workspaces
                                .Include(w => w.Members)
                                .Where(w => w.OrganizationId == organizationId)
                                .ToListAsync();
            return workspaces;
        }
        public async Task<Workspace?> GetWorkspaceByIdAsync(Guid workspaceId)
        {
            return await context.Workspaces.FirstOrDefaultAsync(w => w.Id == workspaceId);
        }

        public async Task<bool> AddMemberToWorkspaceAsync(WorkspaceUserRole workspaceUserRole)
        {
            if(workspaceUserRole is null) return false;

            context.WorkspaceUserRoles.Add(workspaceUserRole);

            var affected = await context.SaveChangesAsync();
            return affected > 0;
        }

        public async Task<List<WorkspaceUserRole>> GetWorkspaceMembers(Guid workspaceId)
        {
            var workspaceUserRoles = await context.WorkspaceUserRoles
                                        .Include(wur => wur.User)
                                        .Include(wur => wur.Role)
                                        .Where(wur => wur.WorkspaceId == workspaceId)
                                        .ToListAsync();
            return workspaceUserRoles;
        }
    }
}
