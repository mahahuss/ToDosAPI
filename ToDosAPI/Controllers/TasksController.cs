using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDosAPI.Extensions;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Services;

namespace ToDosAPI.Controllers;

[Authorize]
public class TasksController : BaseController
{
    private readonly TaskService _taskService;

    public TasksController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllTasks()
    {
        var tasks = await _taskService.GetAllTasksAsync();
        return Ok(tasks);
    }

    [HttpGet("{userId:int}")]
    [AllowAnonymous]
    public async Task<ActionResult> GetAllTasks(int userId)
    {
        var currentUserId = User.GetId();

        if (userId != currentUserId) return Unauthorized("Unauthorized: due to invalid credentials");

        var tasks = await _taskService.GetUserTasksAsync(userId);
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<ActionResult> AddTask([FromForm] AddTaskDto addTaskDto)
    {   
        addTaskDto.CreatedBy = User.GetId();
        var userTask = await _taskService.AddNewTaskAsync(addTaskDto);
        return Ok(userTask);
    }

    [HttpPut]
    public async Task<ActionResult> EditTask(EditTaskDto editTaskDto)
    {
        var check = await _taskService.EditTaskAsync(editTaskDto);
        if (check) return Ok("Updated successfully");

        return BadRequest("Failed to update task");
    }

    [HttpDelete("{taskId}")]
    public async Task<ActionResult> DeleteTask(int taskId)
    {
        var check = await _taskService.DeleteTaskAsync(taskId);

        if (check) return Ok("Deleted successfully");

        return BadRequest("Failed to delete task");
    }

    [HttpGet("attachments/{attachmentId:int}")]
    [AllowAnonymous]
    public async Task<ActionResult> GetTaskAttachment(int attachmentId)
    {
        if (userId != currentUserId) return Unauthorized("Unauthorized: due to invalid credentials");

        var tasks = await _taskService.GetUserTasksAsync(userId);
        return Ok(tasks);
    }
}