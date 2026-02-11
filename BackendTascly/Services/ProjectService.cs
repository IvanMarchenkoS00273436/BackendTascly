using BackendTascly.Entities;
using BackendTascly.Repositories;

namespace BackendTascly.Services
{
    public class ProjectService(IProjectsRepository projectsRepository) : IProjectService
    {
        public Task<bool> CreateProjectAsync(Project project)
        {
            return projectsRepository.AddProjectAsync(project);
        }

        public Task<bool> DeleteProjectAsync(Guid projectId)
        {
            return projectsRepository.DeleteProjectAsync(projectId);
        }

        public Task<List<Project>> GetAllProjectsByOwnerIdAsync(Guid ownerId)
        {
            return projectsRepository.GetAllProjectsByOwnerId(ownerId);
        }

        public Task<Project?> GetProjectByIdAsync(Guid projectId)
        {
            return projectsRepository.GetProjectById(projectId);
        }

        public async Task<List<Project>> GetProjectsByWorkspaceId(Guid workspaceId)
        {
            return await projectsRepository.GetProjectsByWorkspaceId(workspaceId);
        }
    }
}
