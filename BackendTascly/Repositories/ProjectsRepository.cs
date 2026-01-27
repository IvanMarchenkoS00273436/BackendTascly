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
            context.Projects.Remove(project);

            var affected = await context.SaveChangesAsync();
            return affected > 0;
        }

        public async Task<List<Project>> GetAllProjectsByOwnerId(Guid ownerId)
        {
            var projects = await context.Projects
                //.Include(p => p.Members)
                .Include(p => p.TaskStatuses)
                .Include(p => p.TaskImportances)
                .Include(p => p.Tasks)
                //.Include(p => p.Roles)
                .Include(p => p.Owner)
                .Where(p => p.OwnerId == ownerId).ToListAsync();
            return projects;
        }

        public Task<Project?> GetProjectById(Guid projectId)
        {
            var project = context.Projects
                //.Include(p => p.Members)
                .Include(p => p.TaskStatuses)
                .Include(p => p.TaskImportances)
                .Include(p => p.Tasks)
                //.Include(p => p.Roles)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            return project;
        }
    }
}
