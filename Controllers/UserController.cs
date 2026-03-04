using AutoMapper;
using BackendTascly.Data.ModelsDto.UsersDtos;
using BackendTascly.Data.ModelsDto.WorkspaceDtos;
using BackendTascly.Repositories;
using BackendTascly.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendTascly.Controllers
{

    [Authorize]
    [Route("api/Users")]
    [ApiController]
    public class UserController(IUserService userService, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            var organizationId = Guid.Parse(User.FindFirstValue("OrganizationId")!);
            var users = await userService.GetAllUsers(organizationId);

            List<GetUserDto> userDtos = new();

            foreach (var user in users)
            {
                userDtos.Add(mapper.Map<GetUserDto>(user));
            }

            return Ok(userDtos);
        }

        [HttpGet("GetUserProfile")]
        public async Task<ActionResult> GetUserProfile()
        {
            var user = await userService.FindByUserIdAsync(Guid.Parse(User.FindFirstValue("UserId")!));
            GetUserProfile userProfile = mapper.Map<GetUserProfile>(user);

            return Ok(userProfile);
        }

        [HttpPut("UpdateUserProfile")]
        public async Task<ActionResult> PutUserProfile(PutUserProfile putUserProfile)
        {
            var userId = Guid.Parse(User.FindFirstValue("UserId")!);
            await userService.UpdateUserProfileAsync(userId, putUserProfile);

            return Ok();
        }

    }
}
