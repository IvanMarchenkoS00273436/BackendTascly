using BackendTascly.Entities;
using BackendTascly.Data.ModelsDto;
using BackendTascly.Data.ModelsDto.UsersDtos;

namespace BackendTascly.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<TokenResponseDto?> LoginAsync(UserDto request);
        Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request);
    }
}
