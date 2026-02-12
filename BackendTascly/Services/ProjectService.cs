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

            //add default list of Task Statuses to the project
            List<PTaskStatus> taskStatuses = new List<PTaskStatus>()
            {
                new PTaskStatus() { Name = "ToDo" },
                new PTaskStatus() { Name = "InProgress" },
                new PTaskStatus() { Name = "Done" }
            };
            project.TaskStatuses.Concat(taskStatuses);

            //add default list of Task Importance to the project
            List<TaskImportance> taskImportances = new List<TaskImportance>()
            {
                new TaskImportance() { Name = "Low"},
                new TaskImportance() { Name = "Medium"},
                new TaskImportance() { Name = "High"},
            };
            project.TaskImportances.Concat(taskImportances);


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
