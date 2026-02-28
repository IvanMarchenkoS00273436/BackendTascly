using BackendTascly.Entities;

namespace BackendTascly.Repositories
{
    public interface ITaskRepository
    {
        Task<List<PTask>> GetTasksByProjectId(Guid projectId);
        Task<PTask?> GetTaskById(Guid taskId);
        Task<List<PTask>> GetTasksByAssigneeId(Guid assigneeId);
        Task<bool> AddTaskAsync(PTask taskEntity);
        Task<bool> AddTaskAsync(List<PTask> tasks);
        Task<bool> UpdateTaskAsync(PTask taskEntity);
        Task<bool> DeleteTaskAsync(Guid taskId);
    }
}
