
using BackendTascly.Entities;
using BackendTascly.Repositories;

namespace BackendTascly.Services
{
    public class TaskService(ITaskRepository taskRepository, IProjectsRepository projectsRepository, IWorkspaceRepository workspaceRepository) : ITaskService
    {
        public async Task<List<PTask>> GetTasksByProjectId(Guid projectId)
        {
            return await taskRepository.GetTasksByProjectId(projectId);
        }
        public async Task<PTask?> GetTaskById(Guid taskId)
        {
            return await taskRepository.GetTaskById(taskId);
        }

        public async Task<List<PTask>> GetTasksByAssigneeId(Guid assigneeId)
        {
            return await taskRepository.GetTasksByAssigneeId(assigneeId);
        }

        public async Task<bool> CreateTaskAsync(PTask taskEntity, Guid userId, Guid projectId)
        {
            //get project
            var project = await projectsRepository.GetProjectById(projectId);
            if (project is null) return false;

            // only workspace 'Admin' or 'Full-access' can create a Task
            var userRole = await workspaceRepository.GetWorkspaceUserRoleAsync(userId, project.WorkspaceId);
            if (userRole is null || (userRole.Name != "Admin" && userRole.Name != "Full-access"))
                return false;

            taskEntity.ProjectId = projectId; // Task must be created within a Project      
            taskEntity.AuthorId = userId; //assign task Author 
            taskEntity.CreationDate = DateTime.Now;
            taskEntity.LastModifiedDate = DateTime.Now;

            return await taskRepository.AddTaskAsync(taskEntity);
        }

        public async Task<bool> UpdateTaskAsync(Guid taskId, PTask taskEntity, Guid userId)
        {
            return await taskRepository.UpdateTaskAsync(taskEntity);
        }

        public async Task<bool> DeleteTaskAsync(Guid userId, Guid taskId)
        {
            //get an existing task
            var task = await taskRepository.GetTaskById(taskId);
            if (task is null) return false;

            //get project
            var project = await projectsRepository.GetProjectById(task.ProjectId);
            if (project is null) return false;

            // only workspace 'Admin' or 'Full-access' can update a Task
            var userRole = await workspaceRepository.GetWorkspaceUserRoleAsync(userId, project.WorkspaceId);
            if (userRole is null || (userRole.Name != "Admin" && userRole.Name != "Full-access"))
                return false;

            return await taskRepository.DeleteTaskAsync(taskId);
        }
    }
}
