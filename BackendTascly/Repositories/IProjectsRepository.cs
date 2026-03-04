using BackendTascly.Entities;

namespace BackendTascly.Repositories
{
    public interface IProjectsRepository
    {
        Task<Project?> GetProjectById(Guid projectId);
        Task<bool> AddProjectAsync(Project project);
        Task<bool> DeleteProjectAsync(Guid projectId);
        Task<List<Project>> GetProjectsByWorkspaceId(Guid workspaceId);
        Task<List<PTaskStatus>> GetProjectStatuses(Guid projectId);
        Task<PTaskStatus> GetProjectLastStatus(Guid projectId);
        Task<bool> CreateProjectStatus(PTaskStatus taskStatus);
        Task<List<TaskImportance>> GetProjectImportances(Guid projectId);
    }
}
