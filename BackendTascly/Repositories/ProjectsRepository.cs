using BackendTascly.Data;
using BackendTascly.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTascly.Repositories
{
    public class ProjectsRepository(TasclyDbContext context) : IProjectsRepository
    {
        public async Task AddProjectAsync(Project project)
        {
            await context.Projects.AddAsync(project);
        }

        public async Task DeleteProjectAsync(Guid projectId)
        {
            var project = await context.Projects.FindAsync(projectId);
            context.Projects.Remove(project);
        }

        public async Task<List<Project>> GetAllProjectsByOwnerId(Guid ownerId)
        {
            var projects = await context.Projects
                .Include(p => p.Members)
                .Include(p => p.TaskStatuses)
                .Include(p => p.TaskImportances)
                .Include(p => p.Tasks)
                .Include(p => p.Roles)
                .Include(p => p.Owner)
                .Where(p => p.OwnerId == ownerId).ToListAsync();
            return projects;
        }

        public Task<Project?> GetProjectById(Guid projectId)
        {
            var project = context.Projects
                .Include(p => p.Members)
                .Include(p => p.TaskStatuses)
                .Include(p => p.TaskImportances)
                .Include(p => p.Tasks)
                .Include(p => p.Roles)
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            return project;
        }
    }
}
