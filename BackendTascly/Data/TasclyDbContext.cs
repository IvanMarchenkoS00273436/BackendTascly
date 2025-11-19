using BackendTascly.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTascly.Data
{
    public class TasclyDbContext(DbContextOptions<TasclyDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<PTask> Tasks { get; set; }
        public DbSet<PTaskStatus> TaskStatuses { get; set; }
        public DbSet<TaskImportance> TaskImportances { get; set; }
    }
}
