using AutoMapper;
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

    
    }
}
