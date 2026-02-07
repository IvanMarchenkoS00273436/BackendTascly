using AutoMapper;
using BackendTascly.Data.ModelsDto.ProjectsDtos;
using BackendTascly.Data.ModelsDto.WorkspaceDtos;
using BackendTascly.Entities;
using BackendTascly.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendTascly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkspaceController(IWorkspaceService workspaceService) : ControllerBase
    {
        [HttpPost, Authorize]
        public async Task<ActionResult> CreateWorkspaceAsync(PostWorkspaceDto postWorkspaceDto)
        {
            var userId = Guid.Parse(User.FindFirstValue("UserId")!);
            var result = await workspaceService.CreateWorkspaceAsync(postWorkspaceDto, userId);
            if (!result) return BadRequest("Failed to create workspace.");
            return Ok("Workspace created successfully.");
        }
    }
}
