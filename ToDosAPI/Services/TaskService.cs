using Dapper;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using ToDosAPI.Data;
using ToDosAPI.Models.Entities;

namespace ToDosAPI.Services;

public class TaskService 
{

    private readonly UserTaskRepository _userTaskRepo;

    public TaskService(UserTaskRepository userTaskRepo)
    {
        _userTaskRepo = userTaskRepo;
    }
    public async Task<UserTask?> AddNewTaskAsync(UserTask task)
    {
        var createdTask = await _userTaskRepo.CreateTaskAsync(task);
        return createdTask;
    }

    public async Task<bool> DeleteTask(int taskId)
    {
        return await _userTaskRepo.DeleteTaskAsync(taskId);
    }

    public async Task<bool> EditTask(int taskId, string taskContent, int status)
    {
        return await _userTaskRepo.EditTaskAsync(taskId, taskContent, status);
    }

    public async Task<List<UserTask>> GetAllTasks()
    {
        return await _userTaskRepo.GetAllTasksAsync();
    }

    public async Task<List<UserTask>> GetUserTasks(int userId)
    {
        return await _userTaskRepo.GetUserTasksAsync(userId);
    }
}