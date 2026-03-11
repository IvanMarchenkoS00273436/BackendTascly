using BackendTascly.Repositories;

namespace BackendTasclyTests
{
    [TestClass]
    public class UserTests
    {
        private static readonly Guid OrganizationId = Guid.Parse("f8353cae-20ba-44f9-8b8d-99906e8b4319");
        private static readonly Guid UserId1 = Guid.Parse("11111111-1111-1111-1111-111111111111");
        private static readonly Guid UserId2 = Guid.Parse("22222222-2222-2222-2222-222222222222");
        private static readonly Guid UserId3 = Guid.Parse("33333333-3333-3333-3333-333333333333");
        private static readonly Guid UserId4 = Guid.Parse("44444444-4444-4444-4444-444444444444");

        [TestMethod]
        public async Task GetAllUsers_ShouldReturn4Users()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(GetAllUsers_ShouldReturn4Users));
            var repo = new UsersRepository(context);

            var users = await repo.GetAllUsers(OrganizationId);

            Assert.AreEqual(4, users.Count);
        }

        [TestMethod]
        public async Task FindByUserName_ShouldReturnCorrectUser()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(FindByUserName_ShouldReturnCorrectUser));
            var repo = new UsersRepository(context);

            var user = await repo.FindByUserNameAsync("S00273436@atu.ie");

            Assert.IsNotNull(user);
            Assert.AreEqual(UserId1, user.Id);
            Assert.AreEqual("Ivan", user.FirstName);
            Assert.AreEqual("Marchenko", user.LastName);
        }

        [TestMethod]
        public async Task FindByUserName_NonExistent_ShouldReturnNull()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(FindByUserName_NonExistent_ShouldReturnNull));
            var repo = new UsersRepository(context);

            var user = await repo.FindByUserNameAsync("nonexistent@atu.ie");

            Assert.IsNull(user);
        }

        [TestMethod]
        public async Task UserExists_ShouldReturnTrueForSeededUser()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(UserExists_ShouldReturnTrueForSeededUser));
            var repo = new UsersRepository(context);

            var exists = await repo.UserExists("S00274217@atu.ie");

            Assert.IsTrue(exists);
        }

        [TestMethod]
        public async Task UserExists_ShouldReturnFalseForNonExistentUser()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(UserExists_ShouldReturnFalseForNonExistentUser));
            var repo = new UsersRepository(context);

            var exists = await repo.UserExists("unknown@atu.ie");

            Assert.IsFalse(exists);
        }

        [TestMethod]
        public async Task FindByUserId_ShouldReturnUserWithOrganization()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(FindByUserId_ShouldReturnUserWithOrganization));
            var repo = new UsersRepository(context);

            var user = await repo.FindByUserIdAsync(UserId1);

            Assert.IsNotNull(user);
            Assert.IsNotNull(user.Organization);
            Assert.AreEqual(OrganizationId, user.OrganizationId);
        }

        [TestMethod]
        public async Task SeededUsers_ShouldHaveCorrectSuperAdminFlags()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(SeededUsers_ShouldHaveCorrectSuperAdminFlags));
            var repo = new UsersRepository(context);

            var ivan = await repo.FindByUserIdAsync(UserId1);
            var michael = await repo.FindByUserIdAsync(UserId4);

            Assert.IsNotNull(ivan);
            Assert.IsTrue(ivan.IsSuperAdmin);
            Assert.IsNotNull(michael);
            Assert.IsFalse(michael.IsSuperAdmin);
        }

        [TestMethod]
        public async Task SeededUsers_ShouldHaveCorrectNames()
        {
            await using var context = await TestDbHelper.CreateSeededContextAsync(nameof(SeededUsers_ShouldHaveCorrectNames));
            var repo = new UsersRepository(context);

            var user2 = await repo.FindByUserNameAsync("S00274217@atu.ie");
            var user3 = await repo.FindByUserNameAsync("S00236888@atu.ie");
            var user4 = await repo.FindByUserNameAsync("S00299999@atu.ie");

            Assert.IsNotNull(user2);
            Assert.AreEqual("Oleksandr", user2.FirstName);
            Assert.AreEqual("Keshel", user2.LastName);

            Assert.IsNotNull(user3);
            Assert.AreEqual("Ryan", user3.FirstName);
            Assert.AreEqual("Mc Clelland", user3.LastName);

            Assert.IsNotNull(user4);
            Assert.AreEqual("Michael", user4.FirstName);
            Assert.AreEqual("Jackson", user4.LastName);
        }
    }
}
