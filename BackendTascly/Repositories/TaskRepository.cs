using BackendTascly.Data;
using BackendTascly.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTascly.Repositories
{
    public class TaskRepository(TasclyDbContext context) : ITaskRepository
    {
        public async Task<List<PTask>> GetTasksByProjectId(Guid projectId)
        {
            return await context.Tasks
                            .Include(t => t.Status)
                            .Include(t => t.Importance)
                            .Where(t => t.ProjectId == projectId)
                            .ToListAsync();
        }
    }
}
