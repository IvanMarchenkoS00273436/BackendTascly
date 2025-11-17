using BackendTascly.Models;

namespace BackendTascly.Repositories
{
    public interface IUsersRepository
    {
        Task<bool> UserExists(string username);
        Task AddUserAsync(User user);
        Task<User?> FindByUserNameAsync(string username);
        Task SaveChangesAsync();
        Task<User?> FindByUserIdAsync(Guid userId); 
    }
}
