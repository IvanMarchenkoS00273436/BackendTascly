using AutoMapper;
using BackendTascly.Data.ModelsDto.ProjectsDtos;
using BackendTascly.Data.ModelsDto.WorkspaceDtos;
using BackendTascly.Entities;
using BackendTascly.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace BackendTascly.Controllers
{
    [Authorize]
    [Route("api/Workspaces")]
    [ApiController]
    public class WorkspaceController(IWorkspaceService workspaceService, IMapper mapper) : ControllerBase
    {
        [HttpPost]
        // TODO: Authorization - only SuperAdmins can create a Workspace
        public async Task<ActionResult> CreateWorkspace(PostWorkspaceDto postWorkspaceDto)
        {
            var userId = Guid.Parse(User.FindFirstValue("UserId")!);
            var result = await workspaceService.CreateWorkspaceAsync(postWorkspaceDto, userId);
            if (!result) return BadRequest("Failed to create workspace.");
            return Ok("Workspace created successfully.");
        }

        [HttpGet]
        public async Task<ActionResult> GetAllWorkspaces()
        {
            var organizationId = Guid.Parse(User.FindFirstValue("OrganizationId")!);
            var workspaces = await workspaceService.GetAllWorkspacesAsync(organizationId);

            // temporary workspaceDTO using Select
            var workspaceDTOs = workspaces.Select(w => new
            {
                w.Id,
                w.Name,
                w.OrganizationId,
                Members = w.Members.Select(u => u.Username)
            });

            return Ok(workspaceDTOs);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetWorkspaceById(Guid id)
        {
            var workspace = await workspaceService.GetWorkspaceByIdAsync(id);

            if (workspace == null)
                return NotFound();

            var workspaceDto = mapper.Map<GetWorkspace>(workspace);

            return Ok(workspaceDto);
        }

        [HttpPost("{id:guid}/Members")]
        // TODO: Retrieve workspace from Route
        // TODO: Authorization - only SuperAdmins can Add Members to Workspace
        public async Task<ActionResult> AddMemberToWorkspace(PostMemberToWorkspaceDto req, Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue("UserId")!); //who sends the request
            var result = await workspaceService.AddMemberToWorkspaceAsync(req, userId, id);
            if (!result) return BadRequest("Failed to add member to the workspace.");
            return Ok("Member was successfully added to the workspace.");
        }
    }
}
