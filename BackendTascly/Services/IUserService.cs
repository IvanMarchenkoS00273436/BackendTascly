
using BackendTascly.Entities;

namespace BackendTascly.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers(Guid organizationId);
    }
}
