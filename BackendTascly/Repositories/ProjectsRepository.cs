using BackendTascly.Data;
using BackendTascly.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTascly.Repositories
{
    public class ProjectsRepository(TasclyDbContext context) : IProjectsRepository
    {
        public async Task<bool> AddProjectAsync(Project project)
        {
            if (project is null) return false;

            context.Projects.Add(project);

            var affected = await context.SaveChangesAsync();
            return affected > 0; // >0 means at least one entity row was written.
        }

        public async Task<bool> DeleteProjectAsync(Guid projectId)
        {
            var project = await context.Projects.FindAsync(projectId);
            if (project is null) return false;
            
            context.Projects.Remove(project);

            var affected = await context.SaveChangesAsync();
            return affected > 0;
        }

        public Task<Project?> GetProjectById(Guid projectId)
        {
            var project = context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

            return project;
        }

        public async Task<List<Project>> GetProjectsByWorkspaceId(Guid workspaceId)
        {
            return await context.Projects.Where(p => p.WorkspaceId == workspaceId).ToListAsync();
        }
    }
}
