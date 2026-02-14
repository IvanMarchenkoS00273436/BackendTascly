
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
    }
}
