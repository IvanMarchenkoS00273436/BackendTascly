using BackendTascly.Data;
using BackendTascly.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTascly.Repositories
{
    public class UsersRepository(TasclyDbContext context) : IUsersRepository
    {
        public async Task<bool> UserExists(string username)
        {
            return await context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task AddUserAsync(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        public async Task<User?> FindByUserNameAsync(string username)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user;
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task<User?> FindByUserIdAsync(Guid userId)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
    }
}
