using ToDosAPI.Data;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ToDosAPI.Services;

// TODO: CHECK ENTITY EXISTENCE
// TODO: CHECK IF USER IS OWNER

public class TaskService
{
    private readonly UserTaskRepository _userTaskRepo;
    private readonly string _imageDir;

    public TaskService(UserTaskRepository userTaskRepo, IConfiguration configuration)
    {
        
        _userTaskRepo = userTaskRepo;
        _imageDir = configuration.GetValue<string>("Files:FilesPath")!;

    }

    public async Task<UserTask?> AddNewTaskAsync(AddTaskDto task)
    {
        var createdTask = await _userTaskRepo.CreateTaskAsync(task);
        if (createdTask != null)
        {
            
            foreach (var file in task.Files.Where(f => f.ContentType.ToLower() == "application/pdf" || f.ContentType.ToLower() == "image/png"))
            {
                await using var fileStream = new FileStream(Path.Combine(_imageDir, file.FileName), FileMode.Create);
                await file.CopyToAsync(fileStream);
            }
        }
        return createdTask;
    }

    public Task<bool> DeleteTaskAsync(int taskId)
    {
        
        return _userTaskRepo.DeleteTaskAsync(taskId);
    }

    public Task<bool> EditTaskAsync(EditTaskDto editTaskDto)
    {
        return _userTaskRepo.EditTaskAsync(editTaskDto);
    }

    public Task<List<UserTask>> GetAllTasksAsync()
    {
        return _userTaskRepo.GetAllTasksAsync();
    }

    public Task<List<UserTask>> GetUserTasksAsync(int userId)
    {
        return _userTaskRepo.GetUserTasksAsync(userId);
    }
}