using ToDosAPI.Data;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Models.Entities;

namespace ToDosAPI.Services;

// TODO: CHECK ENTITY EXISTENCE
// TODO: CHECK IF USER IS OWNER

public class TaskService
{
    private readonly UserTaskRepository _userTaskRepo;

    public TaskService(UserTaskRepository userTaskRepo)
    {
        _userTaskRepo = userTaskRepo;
    }

    public async Task<UserTask?> AddNewTaskAsync(AddTaskDto task)
    {
        var createdTask = await _userTaskRepo.CreateTaskAsync(task);
        return createdTask;
    }

    public Task<bool> DeleteTaskAsync(int taskId)
    {
        return _userTaskRepo.DeleteTaskAsync(taskId);
    }

    public Task<bool> EditTaskAsync(int taskId, string taskContent, int status)
    {
        return _userTaskRepo.EditTaskAsync(taskId, taskContent, status);
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