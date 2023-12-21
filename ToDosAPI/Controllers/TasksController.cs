using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
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

    [HttpGet]
    public async Task<ActionResult> GetAllTasks()
    {
        var tasks = await _taskService.GetAllTasksAsync();
        return Ok(tasks);
    }

    [HttpGet("{userId:int}")]
    public async Task<ActionResult> GetAllTasks(int userId)
    {
        var currentUserId = User.GetId();
        var roles = User.GetRoles(); // send as a parameters

        if (userId != currentUserId && !roles.Contains("Admin") && !roles.Contains("Moderator"))
            return Unauthorized("Unauthorized: due to invalid credentials");

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

    [HttpPost]
    public async Task<ActionResult> ShareTask(ShareTaskDto shareTaskDto)
    {
        var result = await _taskService.ShareTaskAsync(shareTaskDto);
        if (!result) return BadRequest("Failed to share task");
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult> EditTask(EditTaskDto editTaskDto)
    {
        var task = await _taskService.GetTaskByIdAsync(editTaskDto.Id);

        if (task == null) return NotFound("The Selected Task Not Exist");
        //check also if the task shared with the user 
       // if (task.CreatedBy != User.GetId()) return Unauthorized("Unauthorized: due to invalid credentials");

        var check = await _taskService.EditTaskAsync(editTaskDto);
        if (check) return Ok("Updated successfully");

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