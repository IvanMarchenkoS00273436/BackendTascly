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

        public async Task<List<PTaskStatus>> GetProjectStatuses(Guid projectId)
        {
            return await context.TaskStatuses
                .Where(ts => ts.ProjectId == projectId)
                .OrderByDescending(ts => ts.NextStatusId.HasValue).ThenBy(ts => ts.NextStatusId)
                .ToListAsync();
        }

        public async Task<PTaskStatus> GetProjectLastStatus(Guid projectId)
        {
            // last status in a project has NextStatusId with a value of NULL
            return await context.TaskStatuses.Where(ts => ts.ProjectId == projectId).FirstAsync(ts => ts.NextStatusId == null);
        }

        public async Task<List<TaskImportance>> GetProjectImportances(Guid projectId)
        {
            return await context.TaskImportances.Where(ts => ts.ProjectId == projectId).ToListAsync();
        }

        public async Task<bool> CreateProjectStatus(PTaskStatus taskStatus)
        {
            if (taskStatus is null) return false;

            context.TaskStatuses.Add(taskStatus);

            var affected = await context.SaveChangesAsync();
            return affected > 0; // >0 means at least one entity row was written.
        }
    }
}
