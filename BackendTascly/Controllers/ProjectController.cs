using AutoMapper;
using BackendTascly.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackendTascly.Entities;
using BackendTascly.Entities.ModelsDto.ProjectsDtos;

namespace BackendTascly.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController(IProjectService projectService, IMapper mapper) : ControllerBase
    {
        [HttpGet("by-owner/{ownerId:guid}")]
        public async Task<ActionResult> GetAllProjectsByOwnerIdAsync(Guid ownerId)
        {
            var projects = await projectService.GetAllProjectsByOwnerIdAsync(ownerId);
            var projectsdto = mapper.Map<List<GetProject>>(projects);
            return Ok(projectsdto);
        }
    }
}
