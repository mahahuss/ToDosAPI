using ToDosAPI.Data;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Models.Entities;

namespace ToDosAPI.Services;

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
        //f.ContentType.ToLower() == "application/pdf" || f.ContentType.ToLower() == "image/png")
        var createdTask = await _userTaskRepo.CreateTaskAsync(task);

        if (createdTask == null) return createdTask;

        foreach (var file in task.Files)
        {
            var taskFile = await _userTaskRepo.CreateTaskAttachmentAsync(createdTask.Id, file.FileName);
            if (taskFile != null)
                createdTask.Files.Add(taskFile);

            await using var fileStream = new FileStream(Path.Combine(_imageDir, file.FileName), FileMode.Create);
            await file.CopyToAsync(fileStream);
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