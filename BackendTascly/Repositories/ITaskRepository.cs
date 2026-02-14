using BackendTascly.Entities;

namespace BackendTascly.Repositories
{
    public interface ITaskRepository
    {
        Task<List<PTask>> GetTasksByProjectId(Guid projectId);
    }
}
