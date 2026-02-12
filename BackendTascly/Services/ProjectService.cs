using BackendTascly.Entities;
using BackendTascly.Repositories;

namespace BackendTascly.Services
{
    public class ProjectService(IProjectsRepository projectsRepository) : IProjectService
    {
        public Task<bool> CreateProjectAsync(Project project, Guid userId, Guid workspaceId)
        {
            project.WorkspaceId = workspaceId; // project must be created within a Workspace      
            project.OwnerId = userId; //assign owner to the project

            //add default Task Statuses to the project
            project.TaskStatuses.Add(new PTaskStatus() { Name = "ToDo" });            
            project.TaskStatuses.Add(new PTaskStatus() { Name = "InProgress" });
            project.TaskStatuses.Add(new PTaskStatus() { Name = "Done" });


            //add default Task Importance to the project
            project.TaskImportances.Add(new TaskImportance() { Name = "Low" });
            project.TaskImportances.Add(new TaskImportance() { Name = "Medium" });
            project.TaskImportances.Add(new TaskImportance() { Name = "High" });


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
