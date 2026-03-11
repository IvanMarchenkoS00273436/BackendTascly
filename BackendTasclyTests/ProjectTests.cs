using BackendTascly.Repositories;

namespace BackendTasclyTests
{
    [TestClass]
    public class ProjectTests
    {
        private static readonly Guid WorkspaceId1 = Guid.Parse("92d512e7-9799-4468-a3ea-dbffcc345548");
        private static readonly Guid WorkspaceId2 = Guid.Parse("7af34d3b-3697-4724-851c-9055332d6ee3");
        private static readonly Guid ProjectId1 = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        private static readonly Guid ProjectId2 = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
        private static readonly Guid UserId1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
        private static readonly Guid UserId2 = Guid.Parse("22222222-2222-2222-2222-222222222222");

        [TestMethod]
        public async Task GetProjectsByWorkspaceId_ITDepartment_ShouldReturn1Project()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetProjectsByWorkspaceId_ITDepartment_ShouldReturn1Project));
            var repo = new ProjectsRepository(context);

            var projects = await repo.GetProjectsByWorkspaceId(WorkspaceId1);

            Assert.AreEqual(1, projects.Count);
            Assert.AreEqual("Backend Development", projects[0].Name);
        }

        [TestMethod]
        public async Task GetProjectsByWorkspaceId_MarketingDepartment_ShouldReturn1Project()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetProjectsByWorkspaceId_MarketingDepartment_ShouldReturn1Project));
            var repo = new ProjectsRepository(context);

            var projects = await repo.GetProjectsByWorkspaceId(WorkspaceId2);

            Assert.AreEqual(1, projects.Count);
            Assert.AreEqual("Public Relations", projects[0].Name);
        }

        [TestMethod]
        public async Task GetProjectById_ShouldReturnBackendDevelopment()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetProjectById_ShouldReturnBackendDevelopment));
            var repo = new ProjectsRepository(context);

            var project = await repo.GetProjectById(ProjectId1);

            Assert.IsNotNull(project);
            Assert.AreEqual("Backend Development", project.Name);
            Assert.AreEqual("Backend development project for task management system", project.Description);
            Assert.AreEqual(UserId1, project.OwnerId);
            Assert.AreEqual(WorkspaceId1, project.WorkspaceId);
        }

        [TestMethod]
        public async Task GetProjectById_ShouldReturnPublicRelations()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetProjectById_ShouldReturnPublicRelations));
            var repo = new ProjectsRepository(context);

            var project = await repo.GetProjectById(ProjectId2);

            Assert.IsNotNull(project);
            Assert.AreEqual("Public Relations", project.Name);
            Assert.AreEqual(UserId2, project.OwnerId);
            Assert.AreEqual(WorkspaceId2, project.WorkspaceId);
        }

        [TestMethod]
        public async Task GetProjectStatuses_Project1_ShouldReturn4Statuses()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetProjectStatuses_Project1_ShouldReturn4Statuses));
            var repo = new ProjectsRepository(context);

            var statuses = await repo.GetProjectStatuses(ProjectId1);

            Assert.AreEqual(4, statuses.Count);
            var statusNames = statuses.Select(s => s.Name).OrderBy(n => n).ToList();
            CollectionAssert.AreEqual(
                new[] { "Backlog", "Done", "In Progress", "To Do" },
                statusNames);
        }

        [TestMethod]
        public async Task GetProjectStatuses_Project2_ShouldReturn4Statuses()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetProjectStatuses_Project2_ShouldReturn4Statuses));
            var repo = new ProjectsRepository(context);

            var statuses = await repo.GetProjectStatuses(ProjectId2);

            Assert.AreEqual(4, statuses.Count);
            var statusNames = statuses.Select(s => s.Name).OrderBy(n => n).ToList();
            CollectionAssert.AreEqual(
                new[] { "Backlog", "Done", "In Progress", "To Do" },
                statusNames);
        }

        [TestMethod]
        public async Task GetProjectImportances_Project1_ShouldReturn3Importances()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetProjectImportances_Project1_ShouldReturn3Importances));
            var repo = new ProjectsRepository(context);

            var importances = await repo.GetProjectImportances(ProjectId1);

            Assert.AreEqual(3, importances.Count);
            var importanceNames = importances.Select(i => i.Name).OrderBy(n => n).ToList();
            CollectionAssert.AreEqual(
                new[] { "High", "Low", "Medium" },
                importanceNames);
        }

        [TestMethod]
        public async Task GetProjectImportances_Project2_ShouldReturn3Importances()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetProjectImportances_Project2_ShouldReturn3Importances));
            var repo = new ProjectsRepository(context);

            var importances = await repo.GetProjectImportances(ProjectId2);

            Assert.AreEqual(3, importances.Count);
            var importanceNames = importances.Select(i => i.Name).OrderBy(n => n).ToList();
            CollectionAssert.AreEqual(
                new[] { "High", "Low", "Medium" },
                importanceNames);
        }

        [TestMethod]
        public async Task GetProjectById_NonExistent_ShouldReturnNull()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetProjectById_NonExistent_ShouldReturnNull));
            var repo = new ProjectsRepository(context);

            var project = await repo.GetProjectById(Guid.NewGuid());

            Assert.IsNull(project);
        }
    }
}
