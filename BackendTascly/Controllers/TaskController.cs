using AutoMapper;
using BackendTascly.Data.ModelsDto.ProjectsDtos;
using BackendTascly.Data.ModelsDto.TaskDtos;
using BackendTascly.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendTascly.Controllers
{
    [Authorize]
    [Route("api/Tasks")]
    [ApiController]
    public class TaskController(ITaskService taskService, IMapper mapper): ControllerBase
    {
        [HttpGet("Projects/{projectId}")]
        public async Task<ActionResult> GetProjectTasks(Guid projectId)
        {
            var tasks = await taskService.GetTasksByProjectId(projectId);

            var tasksDto = mapper.Map<List<GetTask>>(tasks);

            return Ok(tasksDto);
        }

        [HttpGet("{taskId}")]
        public async Task<ActionResult> GetTaskById(Guid taskId)
        {
            var task = await taskService.GetTaskById(taskId);
            if (task is null) return NotFound("Task not found.");

            var taskDto = mapper.Map<GetTask>(task);

            return Ok(taskDto);
        }

        [HttpGet]
        public async Task<ActionResult> GetTasksByAssigneeId(Guid assigneeId)
        {
            var tasks = await taskService.GetTasksByAssigneeId(assigneeId);

            var tasksDto = mapper.Map<List<GetTask>>(tasks);

            return Ok(tasksDto);
        }
    }
}
