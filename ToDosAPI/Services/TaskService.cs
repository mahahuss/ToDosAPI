using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using ToDosAPI.Data;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Models.Entities;

namespace ToDosAPI.Services;

public class TaskService
{
    private readonly UserTaskRepository _userTaskRepo;
    private readonly string _filesDir;

    public TaskService(UserTaskRepository userTaskRepo, IConfiguration configuration)
    {
        _userTaskRepo = userTaskRepo;
        _filesDir = configuration.GetValue<string>("Files:FilesPath")!;
    }

    public async Task<UserTask?> AddNewTaskAsync(AddTaskDto task)
    {
        //f.ContentType.ToLower() == "application/pdf" || f.ContentType.ToLower() == "image/png")
        var createdTask = await _userTaskRepo.CreateTaskAsync(task);

        if (createdTask == null || task.Files.Count == 0 ) return createdTask;

        var filePath = Path.Combine(_filesDir, task.CreatedBy.ToString());
        Directory.CreateDirectory(filePath!);

        if (!Directory.Exists(filePath)) return createdTask;

        foreach (var file in task.Files)
        {
            
            var filename = $"{Path.GetFileNameWithoutExtension(file.FileName)}{Guid.NewGuid().ToString("N")}{Path.GetExtension(file.FileName)}";
            var taskFile = await _userTaskRepo.CreateTaskAttachmentAsync(createdTask.Id, filename);
            if (taskFile != null)
            {
                createdTask.Files.Add(taskFile);
                await using var fileStream = new FileStream(Path.Combine(filePath,filename), FileMode.Create);
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

    public Task<TaskAttachment?> GetTaskAttachmentAsync(int attachmentId)
    {
        return _userTaskRepo.GetTaskAttachmentAsync(attachmentId);

    }

    public Task<TasksDto?> GetTaskByIdAsync(int taskId)
    {
        return _userTaskRepo.GetTaskByIdAsync(taskId);
    }
}