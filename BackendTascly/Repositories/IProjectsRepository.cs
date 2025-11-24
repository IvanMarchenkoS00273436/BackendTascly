using BackendTascly.Entities;

namespace BackendTascly.Repositories
{
    public interface IProjectsRepository
    {
        Task<List<Project>> GetAllProjectsByOwnerId(Guid ownerId);
        Task<Project?> GetProjectById(Guid projectId);
        Task AddProjectAsync(Project project);
        Task DeleteProjectAsync(Guid projectId);
    }
}
