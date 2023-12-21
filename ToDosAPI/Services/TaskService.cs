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

    public async Task<bool> DeleteTaskAsync(int taskId)
    {
        return await _userTaskRepo.DeleteTaskAsync(taskId);
    }

    public async Task<bool> EditTaskAsync(EditTaskDto editTaskDto)
    {
        return await _userTaskRepo.EditTaskAsync(editTaskDto);
    }

    public async Task<List<UserTask>> GetAllTasksAsync()
    {
        return await _userTaskRepo.GetAllTasksAsync();
    }

    public async Task<List<UserWithSharedTask>> GetUserTasksAsync(int userId)
    {
        return await _userTaskRepo.GetUserTasksAsync(userId);
    }

    public async Task<TaskAttachment?> GetTaskAttachmentAsync(int attachmentId)
    {
        return await _userTaskRepo.GetTaskAttachmentAsync(attachmentId);
    }

    public async Task<TasksDto?> GetTaskByIdAsync(int taskId)
    {
        return await _userTaskRepo.GetTaskByIdAsync(taskId);
    }

    public async Task<bool> ShareTaskAsync(ShareTaskDto shareTaskDto)
    {
        var created = false;
        foreach (int user in shareTaskDto.SharedTo) {
           var status = await _userTaskRepo.ShareTaskAsync(user, shareTaskDto.TaskId, shareTaskDto.IsEditable);
        }
        return created;
    }
}