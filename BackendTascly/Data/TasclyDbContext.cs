using BackendTascly.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BackendTascly.Data
{
    public class TasclyDbContext(DbContextOptions<TasclyDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleUserProject> RoleUserProjects { get; set; }
        public DbSet<PTask> Tasks { get; set; }
        public DbSet<PTaskStatus> TaskStatuses { get; set; }
        public DbSet<TaskImportance> TaskImportances { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
        }
    }
}
