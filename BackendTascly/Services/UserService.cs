using BackendTascly.BusinessLayer;
using BackendTascly.Data.ModelsDto.UsersDtos;
using BackendTascly.Entities;
using BackendTascly.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BackendTascly.Services
{
    public class UserService(IUsersRepository usersRepository) : IUserService
    {
        public Task<User?> FindByUserIdAsync(Guid userId)
        {
            return usersRepository.FindByUserIdAsync(userId);
        }

        public async Task<List<User>> GetAllUsers(Guid organizationId)
        {
            return await usersRepository.GetAllUsers(organizationId);
        }

        public async Task UpdateUserProfileAsync(Guid userId, PutUserProfile userProfile)
        {
            var user = await usersRepository.FindByUserIdAsync(userId);
            if (user == null) return;

            var updatedInfo = new User
            {
                FirstName = userProfile.FirstName ?? "",
                LastName = userProfile.LastName ?? "",
                Username = userProfile.UserName ?? ""
            };

            UsersBusiness.UpdateUserEntity(user, updatedInfo, userProfile.NewPassword);

            await usersRepository.UpdateUserAsync(user);
        }
    }
}
