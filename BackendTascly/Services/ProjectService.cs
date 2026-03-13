using BackendTascly.Entities;
using BackendTascly.Repositories;

namespace BackendTascly.Services
{
    public class ProjectService(IProjectsRepository projectsRepository, IUsersRepository usersRepository, IWorkspaceRepository workspaceRepository) : IProjectService
    {
        public async Task<bool> CreateProjectAsync(Project project, Guid userId, Guid workspaceId)
        {
            // only workspace 'Admin' can create Project
            var userRole = await workspaceRepository.GetWorkspaceUserRoleAsync(userId, workspaceId);
            if (userRole is null || userRole.Name != "Admin") return false;

            project.WorkspaceId = workspaceId; // project must be created within a Workspace      
            project.OwnerId = userId; //assign owner to the project

            //add default Task Statuses to the project
            project.TaskStatuses.Add(new PTaskStatus() { Name = "Backlog" });            
            project.TaskStatuses.Add(new PTaskStatus() { Name = "ToDo" });
            project.TaskStatuses.Add(new PTaskStatus() { Name = "InProgress" });
            project.TaskStatuses.Add(new PTaskStatus() { Name = "Done" });


            //add default Task Importance to the project
            project.TaskImportances.Add(new TaskImportance() { Name = "Low" });
            project.TaskImportances.Add(new TaskImportance() { Name = "Medium" });
            project.TaskImportances.Add(new TaskImportance() { Name = "High" });


            return await projectsRepository.AddProjectAsync(project);
        }

        public async Task<bool> DeleteProjectAsync(Guid userId, Guid projectId)
        {
            //get project to delete
            var project = await projectsRepository.GetProjectById(projectId);
            if (project is null) return false;

            // only workspace 'Admin' can delete Project
            var userRole = await workspaceRepository.GetWorkspaceUserRoleAsync(userId, project.WorkspaceId);
            if (userRole is null || userRole.Name != "Admin") return false;

            return await projectsRepository.DeleteProjectAsync(projectId);
        }

        public async Task<Project?> GetProjectByIdAsync(Guid projectId)
        {
            return await projectsRepository.GetProjectById(projectId);
        }

        public async Task<List<Project>> GetProjectsByWorkspaceId(Guid workspaceId)
        {
            return await projectsRepository.GetProjectsByWorkspaceId(workspaceId);
        }

        public async Task<List<PTaskStatus>> GetProjectStatuses(Guid projectId)
        {
            return await projectsRepository.GetProjectStatuses(projectId);
        }

        public async Task<List<TaskImportance>> GetProjectImportances(Guid projectId)
        {
            return await projectsRepository.GetProjectImportances(projectId);
        }
    }
}
