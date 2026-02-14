using BackendTascly.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendTascly.Controllers
{
    [Authorize]
    [Route("api/Tasks")]
    [ApiController]
    public class TaskController(ITaskService taskService): ControllerBase
    {
    }
}
