using AutoMapper;
using BackendTascly.Data.ModelsDto.ProjectsDtos;
using BackendTascly.Data.ModelsDto.UsersDtos;
using BackendTascly.Entities;
using BackendTascly.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendTascly.Controllers
{
    [Route("api/Workspaces/{workspaceId}/Projects")]
    [ApiController]
    public class ProjectController(IProjectService projectService, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetWorkspaceProjects(Guid workspaceId)
        {
            var projects = await projectService.GetWorkspaceProjects(workspaceId);

            var projectsDto = mapper.Map<List<GetProject>>(projects);

            return Ok(projectsDto);
        }

        //[HttpGet("by-owner/{ownerId:guid}")]
        //public async Task<ActionResult> GetAllProjectsByOwnerIdAsync(Guid ownerId)
        //{
        //    var projects = await projectService.GetAllProjectsByOwnerIdAsync(ownerId);
        //    var projectsdto = mapper.Map<List<GetProject>>(projects);
        //    return Ok(projectsdto);
        //}

        //[HttpGet("{projectId:guid}")]
        //public async Task<ActionResult> GetProjectByIdAsync(Guid projectId)
        //{
        //    var project = await projectService.GetProjectByIdAsync(projectId);
        //    if (project is null) return NotFound("Project not found.");

        //    var projectdto = mapper.Map<GetProject>(project);
        //    return Ok(projectdto);
        //}

        //[HttpPost]
        //public async Task<ActionResult> CreateProjectAsync(PostProject postProject)
        //{
        //    var projectEntity = mapper.Map<Project>(postProject);
        //    projectEntity.OwnerId = Guid.Parse("22222222-2222-2222-2222-222222222222"); // For now needs to be replaced with actual user id from auth
        //    var result = await projectService.CreateProjectAsync(projectEntity);
        //    if (!result) return BadRequest("Failed to create project.");
        //    return Ok("Project created successfully.");
        //}

        //[HttpDelete("{projectId:guid}")]
        //public async Task<ActionResult> DeleteProjectAsync(Guid projectId)
        //{
        //    var result = await projectService.DeleteProjectAsync(projectId);
        //    if (!result) return NotFound("Project not found or could not be deleted.");
        //    return Ok("Project deleted successfully.");
        //}
    }
}
