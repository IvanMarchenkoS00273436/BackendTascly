using BackendTascly.Data;
using BackendTascly.Data.ModelsDto.UsersDtos;
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
            return await context.Users
                .Include(u => u.Organization)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<List<User>> GetAllUsers(Guid organizationId)
        {
            return await context.Users.Where(u => u.OrganizationId == organizationId).ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }
    }
}
