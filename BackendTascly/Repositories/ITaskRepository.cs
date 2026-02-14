using BackendTascly.Entities;

namespace BackendTascly.Repositories
{
    public interface ITaskRepository
    {
        Task<List<PTask>> GetTasksByProjectId(Guid projectId);
        Task<PTask?> GetTaskById(Guid taskId);
        Task<List<PTask>> GetTasksByAssigneeId(Guid assigneeId);
    }
}
