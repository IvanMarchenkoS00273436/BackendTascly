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
        public async Task<ActionResult> CreateWorkspace(PostWorkspaceDto postWorkspaceDto)
        {
            // authorize User (only SuperAdmins can create workspace) 
            _ = bool.TryParse(User.FindFirstValue("IsSuperAdmin"), out bool isSuperAdmin);
            if (!isSuperAdmin) return Forbid();

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
        public async Task<ActionResult> AddMemberToWorkspace(PostMemberToWorkspaceDto req, Guid id)
        {
            // authorize User (only SuperAdmins can Add Members to Workspace) 
            _ = bool.TryParse(User.FindFirstValue("IsSuperAdmin"), out bool isSuperAdmin);
            if (!isSuperAdmin) return Forbid();

            var userId = Guid.Parse(User.FindFirstValue("UserId")!); //who sends the request
            var result = await workspaceService.AddMemberToWorkspaceAsync(req, userId, id);
            if (!result) return BadRequest("Failed to add member to the workspace.");
            return Ok("Member was successfully added to the workspace.");
        }

        [HttpGet("{id:guid}/Members")]
        public async Task<ActionResult> GetWorkspaceMembers(Guid id)
        {
            var WURs = await workspaceService.GetWorkspaceMembers(id);

            List<GetMemberRoleDto> membersRoles = new();
            foreach (var wur in WURs)
            {
                membersRoles.Add(mapper.Map<GetMemberRoleDto>(wur));
            }

            return Ok(membersRoles);
        }

        [HttpDelete("{workspaceId:guid}/Members/{userId:guid}")]
        public async Task<ActionResult> DeleteMemberFromWorkspace(Guid workspaceId, Guid userId)
        {
            // authorize User (only SuperAdmins can Delete Members from Workspace) 
            _ = bool.TryParse(User.FindFirstValue("IsSuperAdmin"), out bool isSuperAdmin);
            if (!isSuperAdmin) return Forbid();

            bool result = await workspaceService.DeleteUserFromWorkspace(workspaceId, userId);
            if (!result) return BadRequest("Failed to delete member from the workspace.");
            return Ok(result);
        }

        [HttpPut("{workspaceId:guid}/Members/Role")]
        public async Task<ActionResult> UpdateWorkspaceMemberRole(Guid workspaceId, [FromBody] PutMemberWithNewRoleDto putMemberWithNewRoleDto)
        {
            // authorize User (only SuperAdmins can Update Member Role in Workspace) 
            _ = bool.TryParse(User.FindFirstValue("IsSuperAdmin"), out bool isSuperAdmin);
            if (!isSuperAdmin) return Forbid();

            bool result = await workspaceService.UpdateWorkspaceMemberRole(workspaceId, putMemberWithNewRoleDto);
            return Ok(result);
        }

        [HttpGet("{workspaceId:guid}/Members/Role")]
        public async Task<ActionResult> GetWorkspaceMemberRole(Guid workspaceId)
        {
            var userId = Guid.Parse(User.FindFirstValue("UserId")!);

            var role = await workspaceService.GetWorkspaceMemberRole(userId, workspaceId);

            if (role == null)
                return NotFound();

            return Ok(role);
        }

    }
}
