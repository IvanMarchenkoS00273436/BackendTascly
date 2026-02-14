using BackendTascly.Entities;

namespace BackendTascly.Services
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(Project postProject, Guid userId, Guid workspaceId);
        Task<bool> DeleteProjectAsync(Guid projectId);
        Task<Project?> GetProjectByIdAsync(Guid projectId);
        Task<List<Project>> GetProjectsByWorkspaceId(Guid workspaceId);
        Task<List<PTaskStatus>> GetProjectStatuses(Guid projectId);
    }
}
