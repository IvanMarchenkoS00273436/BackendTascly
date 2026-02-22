
using BackendTascly.Entities;
using BackendTascly.Repositories;

namespace BackendTascly.Services
{
    public class TaskService(ITaskRepository taskRepository) : ITaskService
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
            taskEntity.ProjectId = projectId; // Task must be created within a Project      
            taskEntity.AuthorId = userId; //assign task Author 
            taskEntity.CreationDate = DateTime.Now;
            taskEntity.LastModifiedDate = DateTime.Now;

            return await taskRepository.UpdateTaskAsync(taskEntity);
        }

        public async Task<bool> UpdateTaskAsync(Guid taskId, PTask taskEntity, Guid userId)
        {
            return await taskRepository.UpdateTaskAsync(taskEntity);
        }
    }
}
