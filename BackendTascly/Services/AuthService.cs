using BackendTascly.Data;
using BackendTascly.Entities;
using BackendTascly.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BackendTascly.Data.ModelsDto;
using BackendTascly.Data.ModelsDto.UsersDtos;
using BackendTascly.BusinessLayer;

namespace BackendTascly.Services
{
    public class AuthService(IUsersRepository usersRepository, IConfiguration configuration) : IAuthService
    {
        public async Task<TokenResponseDto> LoginAsync(UserDto request)
        {
            var user = await usersRepository.FindByUserNameAsync(request.Username);
            if (user is null) return null;

            if (user.Username != request.Username) return null;

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
                == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return await CreateTokenResponse(user);
        }

        public async Task<(bool, string)> RegisterAsync(PostUserDto request)
        {
            var validationResult = AuthBusiness.IsPostUserDtoValid(request);
            if(validationResult.Item1 == false) return validationResult;
            

            if (await usersRepository.UserExists(request.Username)) return (false, "User already exist");

            var user = new User();

            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, request.Password);

            // AutoMapper in future maybe
            user.Username = request.Username;
            user.PasswordHash = hashedPassword;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Organization = new Organization
            {
                Name = request.OrganizationName
            };
            user.IsSuperAdmin = true;

            await usersRepository.AddUserAsync(user);

            return (true, "User was successfully created");
        }

        private async Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        {
            var user = await usersRepository.FindByUserIdAsync(userId);
            if(user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            return user;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        private async Task<string> GenerateAndSaveRefreshToken(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await usersRepository.SaveChangesAsync();
            return refreshToken;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if(user is null) return null;

            return await CreateTokenResponse(user);
        }

        private async Task<TokenResponseDto> CreateTokenResponse(User? user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshToken(user)
            };
        }
    }
}
