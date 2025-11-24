using BackendTascly.Entities;

namespace BackendTascly.Services
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(Project postProject);
        Task<bool> DeleteProjectAsync(Guid projectId);
        Task<List<Project>> GetAllProjectsByOwnerIdAsync(Guid ownerId);
        Task<Project?> GetProjectByIdAsync(Guid projectId);
    }
}
