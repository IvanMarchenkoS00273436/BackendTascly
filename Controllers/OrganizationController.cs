using BackendTascly.Data.ModelsDto.OrganizationsDtos;
using BackendTascly.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendTascly.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController(IOrganizationService organizationService) : ControllerBase
    {
        [HttpGet("getOrganizationOverview")]
        public async Task<IActionResult> GetOrganizationOverview()
        {
            var organizationId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "OrganizationId")?.Value);
            var organizationOverview = await organizationService.GetOrganizationOverview(organizationId);
            return Ok(organizationOverview);
        }

        [HttpPut("updateOrganization")]
        public async Task<IActionResult> UpdateOrganization(PutOrganization putOrganization)
        {
            var organizationId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "OrganizationId")?.Value);
            var result = await organizationService.UpdateOrganizationAsync(organizationId, putOrganization);
            if (!result) return BadRequest("Failed to update organization.");
            return Ok(true);
        }
    }
}
