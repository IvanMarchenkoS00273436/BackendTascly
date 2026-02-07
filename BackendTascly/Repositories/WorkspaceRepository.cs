using BackendTascly.Data;
using BackendTascly.Entities;

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
    }
}
