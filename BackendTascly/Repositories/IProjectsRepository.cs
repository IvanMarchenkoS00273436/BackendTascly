using BackendTascly.Entities;

namespace BackendTascly.Repositories
{
    public interface IProjectsRepository
    {
        Task<Project?> GetProjectById(Guid projectId);
        Task<bool> AddProjectAsync(Project project);
        Task<bool> DeleteProjectAsync(Guid projectId);
        Task<List<Project>> GetProjectsByWorkspaceId(Guid workspaceId);
    }
}
