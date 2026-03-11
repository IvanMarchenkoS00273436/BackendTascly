using BackendTascly.Repositories;

namespace BackendTasclyTests
{
    [TestClass]
    public class TasksTests
    {
        private static readonly Guid ProjectId1 = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        private static readonly Guid ProjectId2 = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
        private static readonly Guid UserId1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
        private static readonly Guid UserId2 = Guid.Parse("22222222-2222-2222-2222-222222222222");
        private static readonly Guid TaskId1 = Guid.Parse("99999999-1111-1111-1111-111111111111");
        private static readonly Guid TaskId2 = Guid.Parse("99999999-2222-2222-2222-222222222222");
        private static readonly Guid TaskId3 = Guid.Parse("99999999-3333-3333-3333-333333333333");
        private static readonly Guid TaskId4 = Guid.Parse("99999999-4444-4444-4444-444444444444");

        [TestMethod]
        public async Task GetTasksByProjectId_Project1_ShouldReturn3Tasks()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetTasksByProjectId_Project1_ShouldReturn3Tasks));
            var repo = new TaskRepository(context);

            var tasks = await repo.GetTasksByProjectId(ProjectId1);

            Assert.AreEqual(3, tasks.Count);
        }

        [TestMethod]
        public async Task GetTasksByProjectId_Project2_ShouldReturn1Task()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetTasksByProjectId_Project2_ShouldReturn1Task));
            var repo = new TaskRepository(context);

            var tasks = await repo.GetTasksByProjectId(ProjectId2);

            Assert.AreEqual(1, tasks.Count);
            Assert.AreEqual("Prepare press releases", tasks[0].Name);
        }

        [TestMethod]
        public async Task GetTaskById_SetupDatabase_ShouldReturnCorrectDetails()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetTaskById_SetupDatabase_ShouldReturnCorrectDetails));
            var repo = new TaskRepository(context);

            var task = await repo.GetTaskById(TaskId1);

            Assert.IsNotNull(task);
            Assert.AreEqual("Setup database", task.Name);
            Assert.AreEqual("Configure and setup the database schema", task.Description);
            Assert.AreEqual(ProjectId1, task.ProjectId);
            Assert.AreEqual(UserId1, task.AuthorId);
        }

        [TestMethod]
        public async Task GetTaskById_SetupDatabase_ShouldHaveInProgressStatus()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetTaskById_SetupDatabase_ShouldHaveInProgressStatus));
            var repo = new TaskRepository(context);

            var task = await repo.GetTaskById(TaskId1);

            Assert.IsNotNull(task);
            Assert.IsNotNull(task.Status);
            Assert.AreEqual("In Progress", task.Status.Name);
        }

        [TestMethod]
        public async Task GetTaskById_SetupDatabase_ShouldHaveHighImportance()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetTaskById_SetupDatabase_ShouldHaveHighImportance));
            var repo = new TaskRepository(context);

            var task = await repo.GetTaskById(TaskId1);

            Assert.IsNotNull(task);
            Assert.IsNotNull(task.Importance);
            Assert.AreEqual("High", task.Importance.Name);
        }

        [TestMethod]
        public async Task GetTaskById_CreateApiEndpoints_ShouldHaveToDoStatus()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetTaskById_CreateApiEndpoints_ShouldHaveToDoStatus));
            var repo = new TaskRepository(context);

            var task = await repo.GetTaskById(TaskId2);

            Assert.IsNotNull(task);
            Assert.AreEqual("Create API endpoints", task.Name);
            Assert.IsNotNull(task.Status);
            Assert.AreEqual("To Do", task.Status.Name);
        }

        [TestMethod]
        public async Task GetTaskById_PreparePress_ShouldBelongToProject2()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetTaskById_PreparePress_ShouldBelongToProject2));
            var repo = new TaskRepository(context);

            var task = await repo.GetTaskById(TaskId3);

            Assert.IsNotNull(task);
            Assert.AreEqual("Prepare press releases", task.Name);
            Assert.AreEqual(ProjectId2, task.ProjectId);
            Assert.AreEqual(UserId2, task.AuthorId);
            Assert.IsNotNull(task.Importance);
            Assert.AreEqual("Medium", task.Importance.Name);
        }

        [TestMethod]
        public async Task GetTaskById_ImplementAuth_ShouldBeDoneWithCompletionDate()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetTaskById_ImplementAuth_ShouldBeDoneWithCompletionDate));
            var repo = new TaskRepository(context);

            var task = await repo.GetTaskById(TaskId4);

            Assert.IsNotNull(task);
            Assert.AreEqual("Implement authentication", task.Name);
            Assert.IsNotNull(task.Status);
            Assert.AreEqual("Done", task.Status.Name);
            Assert.IsNotNull(task.CompletionDate);
        }

        [TestMethod]
        public async Task GetTaskById_IncompleteTasks_ShouldHaveNullCompletionDate()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetTaskById_IncompleteTasks_ShouldHaveNullCompletionDate));
            var repo = new TaskRepository(context);

            var task1 = await repo.GetTaskById(TaskId1);
            var task2 = await repo.GetTaskById(TaskId2);
            var task3 = await repo.GetTaskById(TaskId3);

            Assert.IsNotNull(task1);
            Assert.IsNull(task1.CompletionDate);
            Assert.IsNotNull(task2);
            Assert.IsNull(task2.CompletionDate);
            Assert.IsNotNull(task3);
            Assert.IsNull(task3.CompletionDate);
        }

        [TestMethod]
        public async Task GetTaskById_NonExistent_ShouldReturnNull()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetTaskById_NonExistent_ShouldReturnNull));
            var repo = new TaskRepository(context);

            var task = await repo.GetTaskById(Guid.NewGuid());

            Assert.IsNull(task);
        }

        [TestMethod]
        public async Task GetTasksByProjectId_ShouldIncludeStatusAndImportanceNavigation()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetTasksByProjectId_ShouldIncludeStatusAndImportanceNavigation));
            var repo = new TaskRepository(context);

            var tasks = await repo.GetTasksByProjectId(ProjectId1);

            foreach (var task in tasks)
            {
                Assert.IsNotNull(task.Status, $"Task '{task.Name}' should have Status loaded.");
                Assert.IsNotNull(task.Importance, $"Task '{task.Name}' should have Importance loaded.");
            }
        }

        [TestMethod]
        public async Task SeededTasks_ShouldHaveCorrectDates()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(SeededTasks_ShouldHaveCorrectDates));
            var repo = new TaskRepository(context);

            var now = new DateTime(2026, 01, 30, 12, 0, 0, DateTimeKind.Utc);

            var task = await repo.GetTaskById(TaskId1);
            Assert.IsNotNull(task);
            Assert.AreEqual(now.AddDays(-5), task.StartDate);
            Assert.AreEqual(now.AddDays(5), task.DueDate);
            Assert.AreEqual(now.AddDays(-5), task.CreationDate);
            Assert.AreEqual(now, task.LastModifiedDate);
        }
    }
}
