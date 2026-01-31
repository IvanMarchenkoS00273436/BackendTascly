using BackendTascly.Entities;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace BackendTascly.Data
{
    public class TasclyDbContext(DbContextOptions<TasclyDbContext> options) : DbContext(options)
    {


        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<WorkspaceUserRole> WorkspaceUserRoles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<PTask> Tasks { get; set; }
        public DbSet<PTaskStatus> TaskStatuses { get; set; }
        public DbSet<TaskImportance> TaskImportances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Workspace>()
            //    .HasOne(u => u.Owner);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Organization)
                .WithMany().OnDelete(DeleteBehavior.NoAction);
        }

        /*
        public async Task SeedDataAsync(CancellationToken cancellationToken = default)
        {
            // If any users exist we assume seeding done (you can refine per table later)
            if (await Users.AnyAsync(cancellationToken))
                return;

            // Fixed IDs (non-identity tables)
            var userId1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var userId2 = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var userId3 = Guid.Parse("33333333-3333-3333-3333-333333333333");

            var projectId1 = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var projectId2 = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            var roleId1 = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
            var roleId2 = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");
            var roleId3 = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
            var roleId4 = Guid.Parse("11111111-2222-3333-4444-555555555555");

            var now = new DateTime(2024, 11, 19, 12, 0, 0, DateTimeKind.Utc);

            // Users
            await Users.AddRangeAsync(new[]
            {
                new User { Id = userId1, Username = "S00273436@atu.ie", FirstName = "Ivan", LastName = "Marchenko", PasswordHash = "AQAAAAIAAYagAAAAEMnll0s4CTJiYJO9ttu2C1xo46oQg+lt/Tort4O/L85sA+zgk2R5UM0TtxYBt+oL9g==" },
                new User { Id = userId2, Username = "S00274217@atu.ie", FirstName = "Oleksandr", LastName = "Keshel", PasswordHash = "AQAAAAIAAYagAAAAEHp2XuzjGRg7jHQWRVUPaa5VuNLd/smiFulgKXhgan20Hc2Df/31AqntrwOSUF2bIA==" },
                new User { Id = userId3, Username = "S00236888@atu.ie", FirstName = "Ryan", LastName = "Mc Clelland", PasswordHash = "AQAAAAIAAYagAAAAEFQaJLV3MWxzT/uYItMLFV5thvTKO1vnjXyJFvbNrqOI+SXZiXKucDhdU/zG6cE64A==" }
            }, cancellationToken);
            await SaveChangesAsync(cancellationToken);

            // Projects
            await Projects.AddRangeAsync(new[]
            {
                new Project { Id = projectId1, Name = "Backend Development", Description = "Backend development project for task management system", OwnerId = userId1 },
                new Project { Id = projectId2, Name = "Frontend Development", Description = "Frontend development project for task management system", OwnerId = userId2 }
            }, cancellationToken);
            await SaveChangesAsync(cancellationToken);

            // Roles
            await Roles.AddRangeAsync(new[]
            {
                new Role { Id = roleId1, Name = "Project Manager", ProjectId = projectId1 },
                new Role { Id = roleId2, Name = "Developer", ProjectId = projectId1 },
                new Role { Id = roleId3, Name = "Designer", ProjectId = projectId2 },
                new Role { Id = roleId4, Name = "Developer", ProjectId = projectId2 }
            }, cancellationToken);
            await SaveChangesAsync(cancellationToken);

            // RoleUserProjects
            await RoleUserProjects.AddRangeAsync(new[]
            {
                new WorkspaceUserRole { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), RoleId = roleId1, UserId = userId1, ProjectId = projectId1 },
                new WorkspaceUserRole { Id = Guid.Parse("dddddddd-1111-2222-3333-444444444444"), RoleId = roleId2, UserId = userId3, ProjectId = projectId1 },
                new WorkspaceUserRole { Id = Guid.Parse("eeeeeeee-1111-2222-3333-444444444444"), RoleId = roleId3, UserId = userId2, ProjectId = projectId2 },
                new WorkspaceUserRole { Id = Guid.Parse("ffffffff-1111-2222-3333-444444444444"), RoleId = roleId4, UserId = userId3, ProjectId = projectId2 }
            }, cancellationToken);
            await SaveChangesAsync(cancellationToken);

            // Task Statuses (IDENTITY: do NOT set Id values explicitly)
            var statusesToAdd = new[]
            {
                new PTaskStatus { Name = "To Do",        ProjectId = projectId1 },
                new PTaskStatus { Name = "In Progress",  ProjectId = projectId1 },
                new PTaskStatus { Name = "Done",         ProjectId = projectId1 },
                new PTaskStatus { Name = "To Do",        ProjectId = projectId2 },
                new PTaskStatus { Name = "In Progress",  ProjectId = projectId2 },
                new PTaskStatus { Name = "Done",         ProjectId = projectId2 }
            };
            await TaskStatuses.AddRangeAsync(statusesToAdd, cancellationToken);
            await SaveChangesAsync(cancellationToken);

            // Task Importances (IDENTITY: do NOT set Id values explicitly)
            var importancesToAdd = new[]
            {
                new TaskImportance { Name = "Low",    ProjectId = projectId1 },
                new TaskImportance { Name = "Medium", ProjectId = projectId1 },
                new TaskImportance { Name = "High",   ProjectId = projectId1 },
                new TaskImportance { Name = "Low",    ProjectId = projectId2 },
                new TaskImportance { Name = "Medium", ProjectId = projectId2 },
                new TaskImportance { Name = "High",   ProjectId = projectId2 }
            };
            await TaskImportances.AddRangeAsync(importancesToAdd, cancellationToken);
            await SaveChangesAsync(cancellationToken);

            // Resolve generated IDs for statuses/importances
            var statusLookup = TaskStatuses
                .Where(s => s.ProjectId == projectId1 || s.ProjectId == projectId2)
                .AsEnumerable()
                .GroupBy(s => s.ProjectId)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToDictionary(s => s.Name, s => s.Id));

            var importanceLookup = TaskImportances
                .Where(i => i.ProjectId == projectId1 || i.ProjectId == projectId2)
                .AsEnumerable()
                .GroupBy(i => i.ProjectId)
                .ToDictionary(
                    g => g.Key,
                    g => g.ToDictionary(i => i.Name, i => i.Id));

            // Tasks (use resolved short IDs)
            await Tasks.AddRangeAsync(new[]
            {
                new PTask
                {
                    Id = Guid.Parse("99999999-1111-1111-1111-111111111111"),
                    Name = "Setup database",
                    Description = "Configure and setup the database schema",
                    ProjectId = projectId1,
                    StatusId = statusLookup[projectId1]["In Progress"],
                    ImportanceId = importanceLookup[projectId1]["High"],
                    StartDate = now.AddDays(-5),
                    DueDate = now.AddDays(5),
                    AuthorId = userId1,
                    CreationDate = now.AddDays(-5),
                    LastModifiedDate = now
                },
                new PTask
                {
                    Id = Guid.Parse("99999999-2222-2222-2222-222222222222"),
                    Name = "Create API endpoints",
                    Description = "Implement REST API endpoints for task management",
                    ProjectId = projectId1,
                    StatusId = statusLookup[projectId1]["To Do"],
                    ImportanceId = importanceLookup[projectId1]["High"],
                    StartDate = now,
                    DueDate = now.AddDays(10),
                    AuthorId = userId1,
                    CreationDate = now,
                    LastModifiedDate = now
                },
                new PTask
                {
                    Id = Guid.Parse("99999999-3333-3333-3333-333333333333"),
                    Name = "Design UI mockups",
                    Description = "Create UI/UX mockups for the application",
                    ProjectId = projectId2,
                    StatusId = statusLookup[projectId2]["To Do"],
                    ImportanceId = importanceLookup[projectId2]["Medium"],
                    StartDate = now.AddDays(-3),
                    DueDate = now.AddDays(7),
                    AuthorId = userId2,
                    CreationDate = now.AddDays(-3),
                    LastModifiedDate = now
                },
                new PTask
                {
                    Id = Guid.Parse("99999999-4444-4444-4444-444444444444"),
                    Name = "Implement authentication",
                    Description = "Add JWT authentication and authorization",
                    ProjectId = projectId1,
                    StatusId = statusLookup[projectId1]["Done"],
                    ImportanceId = importanceLookup[projectId1]["High"],
                    StartDate = now.AddDays(-10),
                    DueDate = now.AddDays(-2),
                    AuthorId = userId1,
                    CreationDate = now.AddDays(-10),
                    CompletionDate = now.AddDays(-2),
                    LastModifiedDate = now.AddDays(-2)
                }
            }, cancellationToken);
            await SaveChangesAsync(cancellationToken);
        }
        */
    }
}
