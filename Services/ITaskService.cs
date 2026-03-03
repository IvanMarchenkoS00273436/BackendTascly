
using BackendTascly.Entities;

namespace BackendTascly.Services
{
    public interface ITaskService
    {
        Task<List<PTask>> GetTasksByProjectId(Guid projectId);
        Task<List<PTask>> GetTasksByAssigneeId(Guid assigneeId);
        Task<PTask?> GetTaskById(Guid taskId);
        Task<bool> CreateTaskAsync(PTask taskEntity, Guid userId, Guid projectId);
        Task<bool> UpdateTaskAsync(Guid taskId, PTask taskEntity, Guid userId);
        Task<bool> DeleteTaskAsync(Guid taskId);
    }
}
