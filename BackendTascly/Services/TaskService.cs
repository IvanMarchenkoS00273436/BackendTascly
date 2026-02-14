
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
    }
}
