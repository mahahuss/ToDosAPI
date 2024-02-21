using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using ToDosAPI.Data;
using ToDosAPI.Models;
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
            var contentType = _fileService.CheckContentType(file.FileName);
            if (contentType is "application/pdf" or "image/png")
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

    public async Task<bool> EditTaskAsync(EditTaskDto oldTask, EditTaskDto editTaskDto, List<IFormFile> files)
    {
        var editResult = await _userTaskRepo.EditTaskAsync(editTaskDto);

        if (editTaskDto.Files.Count == 0)
        {
            await _userTaskRepo.DeleteFileAsync(editTaskDto.Id);
        }

        var filesToDelete = oldTask.Files.Where(x => editTaskDto.Files.Any(y => y.Id == x.Id));

        if (files.Count == 0) return editResult;

        var filePath = Path.Combine(_filesDir, editTaskDto.CreatedBy.ToString());
        Directory.CreateDirectory(filePath!);

        foreach (var file in files)
        {
            var contentType = _fileService.CheckContentType(file.FileName);
            if (contentType is "application/pdf" or "image/png")
            {
                var filename =
                    $"{Path.GetFileNameWithoutExtension(file.FileName)}{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}";

                await using var fileStream = new FileStream(Path.Combine(filePath, filename), FileMode.Create);
                await file.CopyToAsync(fileStream);
                var taskFile = await _userTaskRepo.CreateTaskAttachmentAsync(editTaskDto.Id, filename);
            }
        }


        return editResult;
    }

    public async Task<List<UserTask>> GetAllTasksAsync()
    {
        return await _userTaskRepo.GetAllTasksAsync();
    }

    public Task<GetUserTasksResponse> GetUserTasksAsync(int userId, int pageNumber, int pageSize)
    {
        return _userTaskRepo.GetUserTasksAsync(userId, pageNumber, pageSize);
    }

    public Task<TaskAttachment?> GetTaskAttachmentAsync(int attachmentId)
    {
        return _userTaskRepo.GetTaskAttachmentAsync(attachmentId);
    }

    public Task<EditTaskDto?> GetTaskByIdAsync(int taskId)
    {
        return _userTaskRepo.GetTaskByIdAsync(taskId);
    }

    public Task ShareTaskAsync(ShareTaskDto shareTaskDto, int userId)
    {
        return _userTaskRepo.ShareTaskAsync(shareTaskDto, userId);
    }

    public Task<List<UserTask>> GetUserTasksAsync(int userId)
    {
        return _userTaskRepo.GetUserTasksAsync(userId);
    }
}