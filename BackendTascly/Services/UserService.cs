using BackendTascly.Entities;
using BackendTascly.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BackendTascly.Services
{
    public class UserService(IUsersRepository usersRepository) : IUserService
    {
        public async Task<List<User>> GetAllUsers(Guid organizationId)
        {
            return await usersRepository.GetAllUsers(organizationId);
        }
    }
}
