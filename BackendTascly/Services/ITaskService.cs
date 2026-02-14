
using BackendTascly.Entities;

namespace BackendTascly.Services
{
    public interface ITaskService
    {
        Task<List<PTask>> GetTasksByProjectId(Guid projectId);
        Task<List<PTask>> GetTasksByAssigneeId(Guid assigneeId);
        Task<PTask?> GetTaskById(Guid taskId);
    }
}
