using BackendTascly.Repositories;

namespace BackendTasclyTests
{
    [TestClass]
    public class WorkspaceTests
    {
        private static readonly Guid OrganizationId = Guid.Parse("f8353cae-20ba-44f9-8b8d-99906e8b4319");
        private static readonly Guid WorkspaceId1 = Guid.Parse("92d512e7-9799-4468-a3ea-dbffcc345548");
        private static readonly Guid WorkspaceId2 = Guid.Parse("7af34d3b-3697-4724-851c-9055332d6ee3");
        private static readonly Guid RoleIdAdmin = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");
        private static readonly Guid RoleIdFullAccess = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee");
        private static readonly Guid RoleIdLimitedAccess = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff");
        private static readonly Guid UserId1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
        private static readonly Guid UserId2 = Guid.Parse("22222222-2222-2222-2222-222222222222");
        private static readonly Guid UserId3 = Guid.Parse("33333333-3333-3333-3333-333333333333");

        [TestMethod]
        public async Task GetAllWorkspaces_ShouldReturn2Workspaces()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetAllWorkspaces_ShouldReturn2Workspaces));
            var repo = new WorkspaceRepository(context);

            var workspaces = await repo.GetAllWorkspacesAsync(OrganizationId);

            Assert.AreEqual(2, workspaces.Count);
        }

        [TestMethod]
        public async Task GetWorkspaceById_ShouldReturnITDepartment()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetWorkspaceById_ShouldReturnITDepartment));
            var repo = new WorkspaceRepository(context);

            var workspace = await repo.GetWorkspaceByIdAsync(WorkspaceId1);

            Assert.IsNotNull(workspace);
            Assert.AreEqual("IT Department", workspace.Name);
            Assert.AreEqual(OrganizationId, workspace.OrganizationId);
        }

        [TestMethod]
        public async Task GetWorkspaceById_ShouldReturnMarketingDepartment()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetWorkspaceById_ShouldReturnMarketingDepartment));
            var repo = new WorkspaceRepository(context);

            var workspace = await repo.GetWorkspaceByIdAsync(WorkspaceId2);

            Assert.IsNotNull(workspace);
            Assert.AreEqual("Marketing Department", workspace.Name);
        }

        [TestMethod]
        public async Task GetWorkspaceMembers_ITDepartment_ShouldHave3Members()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetWorkspaceMembers_ITDepartment_ShouldHave3Members));
            var repo = new WorkspaceRepository(context);

            var members = await repo.GetWorkspaceMembers(WorkspaceId1);

            Assert.AreEqual(3, members.Count);
        }

        [TestMethod]
        public async Task GetWorkspaceMembers_MarketingDepartment_ShouldHave2Members()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetWorkspaceMembers_MarketingDepartment_ShouldHave2Members));
            var repo = new WorkspaceRepository(context);

            var members = await repo.GetWorkspaceMembers(WorkspaceId2);

            Assert.AreEqual(2, members.Count);
        }

        [TestMethod]
        public async Task GetWorkspaceMembers_ITDepartment_ShouldHaveCorrectRoles()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetWorkspaceMembers_ITDepartment_ShouldHaveCorrectRoles));
            var repo = new WorkspaceRepository(context);

            var members = await repo.GetWorkspaceMembers(WorkspaceId1);

            var user1Role = members.First(m => m.UserId == UserId1);
            var user2Role = members.First(m => m.UserId == UserId2);
            var user3Role = members.First(m => m.UserId == UserId3);

            Assert.AreEqual(RoleIdAdmin, user1Role.RoleId);
            Assert.AreEqual(RoleIdFullAccess, user2Role.RoleId);
            Assert.AreEqual(RoleIdLimitedAccess, user3Role.RoleId);
        }

        [TestMethod]
        public async Task GetWorkspaceMembers_ShouldIncludeUserAndRoleNavigation()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetWorkspaceMembers_ShouldIncludeUserAndRoleNavigation));
            var repo = new WorkspaceRepository(context);

            var members = await repo.GetWorkspaceMembers(WorkspaceId1);

            foreach (var member in members)
            {
                Assert.IsNotNull(member.User);
                Assert.IsNotNull(member.Role);
            }

            var admin = members.First(m => m.UserId == UserId1);
            Assert.AreEqual("Admin", admin.Role.Name);
            Assert.AreEqual("Ivan", admin.User.FirstName);
        }

        [TestMethod]
        public async Task GetWorkspaceById_NonExistent_ShouldReturnNull()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetWorkspaceById_NonExistent_ShouldReturnNull));
            var repo = new WorkspaceRepository(context);

            var workspace = await repo.GetWorkspaceByIdAsync(Guid.NewGuid());

            Assert.IsNull(workspace);
        }
    }
}
