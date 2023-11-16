using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Models.Entities;
using ToDosAPI.Services;

namespace ToDosAPI.Controllers;

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
        List<UserTask> tasks = await _taskService.GetAllTasksAsync();
        return Ok(tasks);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult> GetAllTasks(int userId)
    {
        List<UserTask> tasks = await _taskService.GetUserTasksAsync(userId);
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<ActionResult> AddTask(AddTaskDto addTaskDto)
    {
        var userTask = await _taskService.AddNewTaskAsync(addTaskDto);
        return Ok(userTask);
    }


    [HttpPut]
    public async Task<ActionResult> EditTask(EditTaskDto editTaskDto)
    {
        var check = await _taskService.EditTaskAsync(editTaskDto.TaskId, editTaskDto.TaskContent, editTaskDto.Status);
        return Ok(new
        {
            status = check
        });
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteTask(DeleteTaskDto TaskId)
    {
        var check = await _taskService.DeleteTaskAsync(TaskId.TaskId);
        return Ok(new
        {
            status = check
        });
    }
}