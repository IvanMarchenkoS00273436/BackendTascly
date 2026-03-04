using BackendTascly.Data.ModelsDto.UsersDtos;
using BackendTascly.Entities;

namespace BackendTascly.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers(Guid organizationId);
        Task<User?> FindByUserIdAsync(Guid userId);
        Task UpdateUserProfileAsync(Guid userId, PutUserProfile userProfile);
    }
}
