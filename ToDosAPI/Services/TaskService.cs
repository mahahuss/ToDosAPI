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
    private readonly FileService _fileService;

    public TaskService(UserTaskRepository userTaskRepo, IConfiguration configuration, FileService fileService)
    {
        _userTaskRepo = userTaskRepo;
        _filesDir = configuration.GetValue<string>("Files:FilesPath")!;
        _fileService = fileService;
    }

    public async Task<UserTask?> AddNewTaskAsync(AddTaskDto task)
    {
        var createdTask = await _userTaskRepo.CreateTaskAsync(task);

        if (createdTask == null || task.Files.Count == 0) return createdTask;

        var filePath = Path.Combine(_filesDir, task.CreatedBy.ToString());
        Directory.CreateDirectory(filePath!);

        foreach (var file in task.Files)
            
        {
            if (_fileService.CheckContentType(file.FileName) == "application/pdf" || _fileService.CheckContentType(file.FileName) == "image/png")
            {
                var filename =
                    $"{Path.GetFileNameWithoutExtension(file.FileName)}{Guid.NewGuid().ToString("N")}{Path.GetExtension(file.FileName)}";

                await using var fileStream = new FileStream(Path.Combine(filePath, filename), FileMode.Create);
                await file.CopyToAsync(fileStream);
                var taskFile = await _userTaskRepo.CreateTaskAttachmentAsync(createdTask.Id, filename);
                createdTask.Files.Add(taskFile);
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