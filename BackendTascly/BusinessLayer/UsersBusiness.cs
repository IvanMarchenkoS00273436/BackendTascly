using BackendTascly.Entities;
using Microsoft.AspNetCore.Identity;

namespace BackendTascly.BusinessLayer
{
    public static class UsersBusiness
    {
        public static User UpdateUserEntity(User currentUser, User updatedInfo, string? newPassword)
        {
            if (!string.IsNullOrWhiteSpace(updatedInfo.FirstName))
            {
                currentUser.FirstName = updatedInfo.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(updatedInfo.LastName))
            {
                currentUser.LastName = updatedInfo.LastName;
            }

            if (!string.IsNullOrWhiteSpace(updatedInfo.Username))
            {
                currentUser.Username = updatedInfo.Username;
            }

            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                var passwordHasher = new PasswordHasher<User>();
                currentUser.PasswordHash = passwordHasher.HashPassword(currentUser, newPassword);
            }

            return currentUser;
        }
    }
}
