using BackendTascly.Controllers;
using BackendTascly.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Users
            var user1 = new User
            {
                Id = Guid.NewGuid(),
                Username = "S00273436@atu.ie",
                FirstName = "Ivan",
                LastName = "Marchenko",
                PasswordHash = "AQAAAAIAAYagAAAAEMnll0s4CTJiYJO9ttu2C1xo46oQg+lt/Tort4O/L85sA+zgk2R5UM0TtxYBt+oL9g==",
                RefreshToken = null,
                RefreshTokenExpiryTime = null
            };

            var user2 = new User
            {
                Id = Guid.NewGuid(),
                Username = "S00274217@atu.ie",
                FirstName = "Oleksandr",
                LastName = "Keshel",
                PasswordHash = "AQAAAAIAAYagAAAAEHp2XuzjGRg7jHQWRVUPaa5VuNLd/smiFulgKXhgan20Hc2Df/31AqntrwOSUF2bIA==",
                RefreshToken = null,
                RefreshTokenExpiryTime = null
            };

            var user3 = new User
            {
                Id = Guid.NewGuid(),
                Username = "S00236888@atu.ie",
                FirstName = "Ryan",
                LastName = "Mc Clelland",
                PasswordHash = "AQAAAAIAAYagAAAAEFQaJLV3MWxzT/uYItMLFV5thvTKO1vnjXyJFvbNrqOI+SXZiXKucDhdU/zG6cE64A==",
                RefreshToken = null,
                RefreshTokenExpiryTime = null
            };

            modelBuilder.Entity<User>().HasData(user1, user2, user3);


            // Seed Projects
            var project1 = new Project
            {
                Id = Guid.NewGuid(),
                Name = "Backend Development",
                Description = "Backend development project for task management system",
                OwnerId = user1.Id
            };

            var project2 = new Project
            {
                Id = Guid.NewGuid(),
                Name = "Frontend Development",
                Description = "Frontend development project for task management system",
                OwnerId = user2.Id
            };

            modelBuilder.Entity<Project>().HasData(project1, project2);

            // Seed Roles
            var role1 = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Project Manager",
                ProjectId = project1.Id
            };

            var role2 = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Developer",
                ProjectId = project1.Id
            };

            var role3 = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Designer",
                ProjectId = project2.Id
            };

            var role4 = new Role
            {
                Id = Guid.NewGuid(),
                Name = "Developer",
                ProjectId = project2.Id
            };

            modelBuilder.Entity<Role>().HasData(role1, role2, role3, role4);

            // Seed RoleUserProjects
            modelBuilder.Entity<RoleUserProject>().HasData(
                new RoleUserProject
                {
                    Id = Guid.NewGuid(),
                    RoleId = role1.Id,
                    UserId = user1.Id,
                    ProjectId = project1.Id
                },
                new RoleUserProject
                {
                    Id = Guid.NewGuid(),
                    RoleId = role2.Id,
                    UserId = user3.Id,
                    ProjectId = project1.Id
                },
                new RoleUserProject
                {
                    Id = Guid.NewGuid(),
                    RoleId = role3.Id,
                    UserId = user2.Id,
                    ProjectId = project2.Id
                },
                new RoleUserProject
                {
                    Id = Guid.NewGuid(),
                    RoleId = role4.Id,
                    UserId = user3.Id,
                    ProjectId = project2.Id
                }
            );

            // Seed Task Statuses
            modelBuilder.Entity<PTaskStatus>().HasData(
                new PTaskStatus
                {
                    Id = 1,
                    Name = "To Do",
                    ProjectId = project1.Id
                },
                new PTaskStatus
                {
                    Id = 2,
                    Name = "In Progress",
                    ProjectId = project1.Id
                },
                new PTaskStatus
                {
                    Id = 3,
                    Name = "Done",
                    ProjectId = project1.Id
                },
                new PTaskStatus
                {
                    Id = 4,
                    Name = "To Do",
                    ProjectId = project2.Id
                },
                new PTaskStatus
                {
                    Id = 5,
                    Name = "In Progress",
                    ProjectId = project2.Id
                },
                new PTaskStatus
                {
                    Id = 6,
                    Name = "Done",
                    ProjectId = project2.Id
                }
            );

            // Seed Task Importances
            modelBuilder.Entity<TaskImportance>().HasData(
                new TaskImportance
                {
                    Id = 1,
                    Name = "Low",
                    ProjectId = project1.Id
                },
                new TaskImportance
                {
                    Id = 2,
                    Name = "Medium",
                    ProjectId = project1.Id
                },
                new TaskImportance
                {
                    Id = 3,
                    Name = "High",
                    ProjectId = project1.Id
                },
                new TaskImportance
                {
                    Id = 4,
                    Name = "Low",
                    ProjectId = project2.Id
                },
                new TaskImportance
                {
                    Id = 5,
                    Name = "Medium",
                    ProjectId = project2.Id
                },
                new TaskImportance
                {
                    Id = 6,
                    Name = "High",
                    ProjectId = project2.Id
                }
            );

            // Seed Tasks
            var now = new DateTime(2024, 11, 19, 12, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<PTask>().HasData(
                new PTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Setup database",
                    Description = "Configure and setup the database schema",
                    StatusId = 2,
                    ImportanceId = 3,
                    ProjectId = project1.Id,
                    StartDate = now.AddDays(-5),
                    DueDate = now.AddDays(5),
                    AuthorId = user1.Id,
                    CreationDate = now.AddDays(-5),
                    CompletionDate = null,
                    LastModifiedDate = now
                },
                new PTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Create API endpoints",
                    Description = "Implement REST API endpoints for task management",
                    StatusId = 1,
                    ImportanceId = 3,
                    ProjectId = project1.Id,
                    StartDate = now,
                    DueDate = now.AddDays(10),
                    AuthorId = user1.Id,
                    CreationDate = now,
                    CompletionDate = null,
                    LastModifiedDate = now
                },
                new PTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Design UI mockups",
                    Description = "Create UI/UX mockups for the application",
                    StatusId = 4,
                    ImportanceId = 2,
                    ProjectId = project2.Id,
                    StartDate = now.AddDays(-3),
                    DueDate = now.AddDays(7),
                    AuthorId = user2.Id,
                    CreationDate = now.AddDays(-3),
                    CompletionDate = null,
                    LastModifiedDate = now
                },
                new PTask
                {
                    Id = Guid.NewGuid(),
                    Name = "Implement authentication",
                    Description = "Add JWT authentication and authorization",
                    StatusId = 3,
                    ImportanceId = 3,
                    ProjectId = project1.Id,
                    StartDate = now.AddDays(-10),
                    DueDate = now.AddDays(-2),
                    AuthorId = user1.Id,
                    CreationDate = now.AddDays(-10),
                    CompletionDate = now.AddDays(-2),
                    LastModifiedDate = now.AddDays(-2)
                }
            );
        }
    }
}
