using BackendTascly.Data.ModelsDto.AiDtos;
using BackendTascly.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace BackendTascly.Controllers
{
    [Authorize]
    [Route("api/Ai")]
    [ApiController]

    public class AiController(IAiService aiService) : ControllerBase
    {
        // Sends the userr's prompt to Groq and returns a list of draft tasks
        [HttpPost("generate-tasks")]
        public async Task<ActionResult<AiGenerateResponse>> GenerateTasks(AiGenerateRequest request)
        {
            try
            {
                //Override the UserId from the JWT token so it's always correct
                request.UserId = Guid.Parse(User.FindFirstValue("UserId")!);
                var result = await aiService.GenerateTasksAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(500, new { messages = ex.Message });
            }
        }

        [HttpPost("bulk-create")]
        public async Task<ActionResult> BulkCreateTasks(BulkCreateRequest request)
        {
            var userId = Guid.Parse(User.FindFirstValue("UserId")!);
            var result = await aiService.BulkCreateTasksAsync(request, userId);
            if (!result) return BadRequest("Failed to create tasks.");
            return Ok(new { message = "Tasks created successfully." });
        }
    }
}
