using BackendTascly.Data.ModelsDto.UsersDtos;
using BackendTascly.Entities;

namespace BackendTascly.Repositories
{
    public interface IUsersRepository
    {
        Task<bool> UserExists(string username);
        Task AddUserAsync(User user);
        Task<User?> FindByUserNameAsync(string username);
        Task SaveChangesAsync();
        Task<User?> FindByUserIdAsync(Guid userId);
        Task<List<User>> GetAllUsers(Guid organizationId);
        Task UpdateUserAsync(User user);
    }
}
