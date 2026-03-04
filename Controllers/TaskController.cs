using AutoMapper;
using BackendTascly.Data.ModelsDto.ProjectsDtos;
using BackendTascly.Data.ModelsDto.TaskDtos;
using BackendTascly.Entities;
using BackendTascly.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpPost("Projects/{projectId}")]
        public async Task<ActionResult> CreateTask(PostTask postTask, Guid projectId)
        {
            var taskEntity = mapper.Map<PTask>(postTask);
            var userId = Guid.Parse(User.FindFirstValue("UserId")!);

            var result = await taskService.CreateTaskAsync(taskEntity, userId, projectId);
            if (!result) return BadRequest("Failed to create Task.");
            return Ok("Task created successfully.");
        }

        [HttpPatch("{taskId}")]
        public async Task<ActionResult> UpdateTask(
            [FromBody] JsonPatchDocument<UpdateTaskDto> jsonPatch, Guid taskId
            )
        {
            var task = await taskService.GetTaskById(taskId); // get existing task for update

            if (task is null) return NotFound();

            var updateTaskDto = mapper.Map<UpdateTaskDto>(task); // extract updateTaskDto from existing task

            jsonPatch.ApplyTo(updateTaskDto, ModelState); //apply JsonPatchDocument<UpdateTaskDto> changes to updateTaskDto

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedTask = mapper.Map(updateTaskDto, task); // update task with data from updateTaskDto

            var userId = Guid.Parse(User.FindFirstValue("UserId")!);

            var result = await taskService.UpdateTaskAsync(taskId, updatedTask, userId);
            if (!result) return BadRequest("Failed to update Task.");
            return Ok("Task updated successfully.");
        }

        [HttpDelete("{taskId}")]
        public async Task<ActionResult> DeleteTask(Guid taskId)
        {
            var result = await taskService.DeleteTaskAsync(taskId);
            if (!result) return NotFound("Task not found.");
            return Ok("Task deleted successfully.");
        }
    }
}
