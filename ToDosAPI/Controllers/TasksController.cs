using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.StaticFiles;
using ToDosAPI.Extensions;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Services;
using ToDosAPI.Models;

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

    [HttpGet("user-tasks")]
    public async Task<ActionResult> GetAllTasks(int userId, int pageNumber, int pageSize)
    {
        var currentUserId = User.GetId();
        var roles = User.GetRoles();
        var result = await _taskService.GetUserTasksAsync(userId, pageNumber, pageSize, currentUserId, roles);
        return result.Match<ActionResult>(Ok, Unauthorized);
    }

    [HttpGet("user-tasks-only/{userId}")]
    [Authorize(Roles = "Admin, Moderator")]
    public async Task<ActionResult> GetUserTasks(int userId)
    {
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
        // how to send different functions when response is failure? (NotFound(), Unauthorized() ...etc).
        var currentUserId = User.GetId();
        var result = await _taskService.EditTaskAsync(editTaskFormDto, editTaskFormDto.Files, currentUserId);
        return result.Match<ActionResult>(Ok, BadRequest);
    }

    [HttpDelete("{taskId}")]
    public async Task<ActionResult> DeleteTask(int taskId)
    {
        var currentUserId = User.GetId();
        var result = await _taskService.DeleteTaskAsync(taskId, currentUserId);
        return result.Match<ActionResult>(Ok, BadRequest);

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