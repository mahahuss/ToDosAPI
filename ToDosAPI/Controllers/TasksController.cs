using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.StaticFiles;
using ToDosAPI.Extensions;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Services;

namespace ToDosAPI.Controllers;

[Authorize]
public class TasksController : BaseController
{
    private readonly TaskService _taskService;
    private readonly string _filesDir;

    public TasksController(TaskService taskService, IConfiguration configuration)
    {
        _taskService = taskService;
        _filesDir = configuration.GetValue<string>("Files:FilesPath")!;
    }

    //[HttpGet]
    //public async Task<ActionResult> GetAllTasks()
    //{
    //    var tasks = await _taskService.GetAllTasksAsync();
    //    return Ok(tasks);
    //}

    [HttpGet("user-tasks")]
    public async Task<ActionResult> GetAllTasks(int userId, int pageNumber, int pageSize)
    {
        var currentUserId = User.GetId();
        var roles = User.GetRoles(); // send as a parameters

        if (userId != currentUserId && !roles.Contains("Admin") && !roles.Contains("Moderator"))
            return Unauthorized("Unauthorized: due to invalid credentials");

        var tasks = await _taskService.GetUserTasksAsync(userId, pageNumber, pageSize);
        return Ok(tasks);
    }

    [HttpGet("user-tasks-only/{userId}")]
    [Authorize(Roles = "Admin, Moderator")]
    public async Task<ActionResult> GetUserTasks(int userId)
    {
        //var roles = User.GetRoles(); // send as a parameters

        //if (!roles.Contains("Admin") && !roles.Contains("Moderator"))
        //    return Unauthorized("Unauthorized: due to invalid credentials");

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

    [HttpPost("share")]
    public async Task<ActionResult> ShareTask(ShareTaskDto shareTaskDto)
    {
        await _taskService.ShareTaskAsync(shareTaskDto, User.GetId());
        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> EditTask([FromForm] EditTaskFormDto editTaskFormDto)
    {

        var currentUserId = User.GetId();
        var editTaskDto = JsonSerializer.Deserialize<EditTaskDto>(editTaskFormDto.Task);

        if (editTaskDto is null) return BadRequest("Bad task JSON"); // ?

        var task = await _taskService.GetTaskByIdAsync(editTaskDto.Id);

        if (task == null) return NotFound("The Selected Task Not Exist");

        var isItShared = task.SharedTasks.Find(user => user.Id == currentUserId);
        if (task.CreatedBy != User.GetId() && isItShared == null) return Unauthorized("Unauthorized: due to invalid credentials");

        var result = await _taskService.EditTaskAsync(task, editTaskDto, editTaskFormDto.Files);
        if (result) return Ok("Updated successfully");

        return BadRequest("Failed to update task");
    }

    [HttpDelete("{taskId}")]
    public async Task<ActionResult> DeleteTask(int taskId)
    {
        var task = await _taskService.GetTaskByIdAsync(taskId);

        if (task == null) return NotFound("The Selected Task Not Exist");
        if (task.CreatedBy != User.GetId()) return Unauthorized("Unauthorized: due to invalid credentials");

        var check = await _taskService.DeleteTaskAsync(taskId);
        if (check) return Ok("Deleted successfully");

        return BadRequest("Failed to delete task");
    }

    [HttpGet("attachments/{attachmentId:int}")]
    public async Task<ActionResult> GetTaskAttachment(int attachmentId)
    {
        var file = await _taskService.GetTaskAttachmentAsync(attachmentId);
        if (file == null) return NotFound();

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), _filesDir, User.GetId().ToString(), file.FileName);
        if (!System.IO.File.Exists(filePath)) return NotFound();

        new FileExtensionContentTypeProvider().TryGetContentType(file.FileName, out var contentType);

        if (string.IsNullOrEmpty(contentType)) contentType = "application/octet-stream";

        return PhysicalFile(filePath, contentType);
    }
}