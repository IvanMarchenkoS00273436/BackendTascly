
using BackendTascly.Entities;

namespace BackendTascly.Services
{
    public interface ITaskService
    {
        Task<List<PTask>> GetTasksByProjectId(Guid projectId);
        Task<PTask?> GetTaskById(Guid taskId);
    }
}
