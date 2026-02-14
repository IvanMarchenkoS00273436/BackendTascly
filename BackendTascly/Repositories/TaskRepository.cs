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

        public async Task<PTask?> GetTaskById(Guid taskId)
        {
            return await context.Tasks
                            .Include(t => t.Status)
                            .Include(t => t.Importance)
                            .FirstOrDefaultAsync(t => t.Id == taskId);
        }

        public async Task<List<PTask>> GetTasksByAssigneeId(Guid assigneeId)
        {
            return await context.Tasks
                            .Include(t => t.Status)
                            .Include(t => t.Importance)
                            .Where(t => t.AssigneeId == assigneeId)
                            .ToListAsync();
        }

        public async Task<bool> AddTaskAsync(PTask task)
        {
            if (task is null) return false;

            context.Tasks.Add(task);

            var affected = await context.SaveChangesAsync();
            return affected > 0; 
        }
    }
}
