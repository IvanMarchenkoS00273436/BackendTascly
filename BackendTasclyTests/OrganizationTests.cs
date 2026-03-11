using BackendTascly.Repositories;

namespace BackendTasclyTests
{
    [TestClass]
    public class OrganizationTests
    {
        private static readonly Guid OrganizationId = Guid.Parse("f8353cae-20ba-44f9-8b8d-99906e8b4319");

        [TestMethod]
        public async Task GetOrganization_ShouldReturnSeededOrganization()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetOrganization_ShouldReturnSeededOrganization));
            var repo = new OrganizationRepository(context);

            var org = await repo.GetOrganization(OrganizationId);

            Assert.IsNotNull(org);
            Assert.AreEqual("ATU Sligo", org.Name);
        }

        [TestMethod]
        public async Task GetOrganization_ShouldHave4Members()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetOrganization_ShouldHave4Members));
            var repo = new OrganizationRepository(context);

            var org = await repo.GetOrganization(OrganizationId);

            Assert.IsNotNull(org);
            Assert.AreEqual(4, org.Members.Count);
        }

        [TestMethod]
        public async Task GetOrganization_ShouldHave2Workspaces()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetOrganization_ShouldHave2Workspaces));
            var repo = new OrganizationRepository(context);

            var org = await repo.GetOrganization(OrganizationId);

            Assert.IsNotNull(org);
            Assert.AreEqual(2, org.Workspaces.Count);
        }

        [TestMethod]
        public async Task GetOrganization_ShouldIncludeProjectsWithTasks()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetOrganization_ShouldIncludeProjectsWithTasks));
            var repo = new OrganizationRepository(context);

            var org = await repo.GetOrganization(OrganizationId);

            Assert.IsNotNull(org);
            var allProjects = org.Workspaces.SelectMany(w => w.Projects).ToList();
            Assert.AreEqual(2, allProjects.Count);

            var allTasks = allProjects.SelectMany(p => p.Tasks).ToList();
            Assert.AreEqual(4, allTasks.Count);
        }

        [TestMethod]
        public async Task GetOrganization_NonExistent_ShouldReturnNull()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetOrganization_NonExistent_ShouldReturnNull));
            var repo = new OrganizationRepository(context);

            var org = await repo.GetOrganization(Guid.NewGuid());

            Assert.IsNull(org);
        }
    }
}
