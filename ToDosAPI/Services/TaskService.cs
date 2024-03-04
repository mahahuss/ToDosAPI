using Microsoft.AspNetCore.StaticFiles;
using System.Text.Json;
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
    private readonly UserRepository _userRepo;

    public TaskService(UserTaskRepository userTaskRepo, IConfiguration configuration, FileService fileService,
        UserRepository userRepo)
    {
        _userTaskRepo = userTaskRepo;
        _filesDir = configuration.GetValue<string>("Files:FilesPath")!;
        _fileService = fileService;
        _userRepo = userRepo;
    }

    public async Task<UserTask?> AddNewTaskAsync(AddTaskDto task)
    {
        var createdTask = await _userTaskRepo.CreateTaskAsync(task);

        if (createdTask == null || task.Files.Count == 0) return createdTask;

        var filePath = Path.Combine(_filesDir, task.CreatedBy.ToString());
        Directory.CreateDirectory(filePath);

        foreach (var file in task.Files)
        {
            var contentType = _fileService.CheckContentType(file.FileName);
            if (contentType is "application/pdf" or "image/png")
            {
                var filename =
                    $"{Path.GetFileNameWithoutExtension(file.FileName)}{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}";

                await using var fileStream = new FileStream(Path.Combine(filePath, filename), FileMode.Create);
                await file.CopyToAsync(fileStream);
                var taskFile = await _userTaskRepo.CreateTaskAttachmentAsync(createdTask.Id, filename);
                createdTask.Files.Add(taskFile);
            }
        }

        return createdTask;
    }

    public async Task<Result<string>> DeleteTaskAsync(int taskId, int currentUserId)
    {
        var task = await GetTaskByIdAsync(taskId);

        if (task == null) return Result<string>.Failure("The Selected Task Not Exist");
        if (task.CreatedBy != currentUserId)
            return Result<string>.Failure("Unauthorized: You don't have permission to edit task");

        var result = await _userTaskRepo.DeleteTaskAsync(taskId);
        return result
            ? Result<string>.Successful("Task Deleted Successfully")
            : Result<string>.Failure("Unauthorized: You don't have permission to edit task");
    }

    public async Task<Result<UserWithSharedTask>> EditTaskAsync(EditTaskFormDto editTaskFormDto, List<IFormFile> files,
        int currentUserId)
    {
        var editTaskDto = JsonSerializer.Deserialize<EditTaskDto>(editTaskFormDto.Task);

        if (editTaskDto is null) return Result<UserWithSharedTask>.Failure("Failed to Serialize JSON");

        var oldTasks = await GetTaskByIdAsync(editTaskDto.Id);

        if (oldTasks == null) return Result<UserWithSharedTask>.Failure("The Selected Task Not Exist", ResultErrorType.NotFound);

        var isShared = oldTasks.SharedTasks.FirstOrDefault(user => user.SharedWith == currentUserId);

        if (oldTasks.CreatedBy != currentUserId && isShared is null)
            return Result<UserWithSharedTask>.Failure("Unauthorized: You don't have permission to edit task",
                ResultErrorType.Unauthorized);

        if (isShared is not null)
        {
            if (!isShared.IsEditable)
                return Result<UserWithSharedTask>.Failure("Unauthorized: You don't have permission to edit task",
                    ResultErrorType.Unauthorized);
        }

        await _userTaskRepo.EditTaskAsync(editTaskDto);
        if (editTaskDto.Files.Count == 0)
        {
            foreach (var file in oldTasks.Files)
            {
                await _userTaskRepo.DeleteFileAsync(file.Id);
            }
        }
        else
        {
            var filesIdsToDelete = oldTasks.Files.Select(a => a.Id).Except(editTaskDto.Files.Select(b => b.Id));
            var filesToDelete = oldTasks.Files.Where(x => filesIdsToDelete.Any(y => y == x.Id));

            foreach (var file in filesToDelete)
            {
                await _userTaskRepo.DeleteFileAsync(file.Id);
            }
        }

        if (files.Count == 0)
        {
            var result = await GetTaskByIdAsync(editTaskDto.Id);
            return result ?? Result<UserWithSharedTask>.Failure("Failed to Retrieve User Updated Information");
        }

        var filePath = Path.Combine(_filesDir, editTaskDto.CreatedBy.ToString());
        Directory.CreateDirectory(filePath);

        foreach (var file in files)
        {
            var contentType = _fileService.CheckContentType(file.FileName);
            if (contentType is "application/pdf" or "image/png")
            {
                var filename =
                    $"{Path.GetFileNameWithoutExtension(file.FileName)}{Guid.NewGuid():N}{Path.GetExtension(file.FileName)}";

                await using var fileStream = new FileStream(Path.Combine(filePath, filename), FileMode.Create);
                await file.CopyToAsync(fileStream);
                await _userTaskRepo.CreateTaskAttachmentAsync(editTaskDto.Id, filename);
            }
        }

        var finalResult = await GetTaskByIdAsync(editTaskDto.Id);
        return finalResult ?? Result<UserWithSharedTask>.Failure("Failed to Retrieve User Updated Information");
    }

    public async Task<List<UserTask>> GetAllTasksAsync()
    {
        return await _userTaskRepo.GetAllTasksAsync();
    }

    public async Task<Result<GetUserTasksResponse>> GetUserTasksAsync(int userId, int pageNumber, int pageSize, int currentUserId,
        List<string> roles)
    {
        if (userId != currentUserId && !roles.Contains("Admin") && !roles.Contains("Moderator"))
            return Result<GetUserTasksResponse>.Failure("Unauthorized: due to invalid credentials");

        var result = await _userTaskRepo.GetUserTasksAsync(userId, pageNumber, pageSize);
        return result;
    }

    public async Task<Result<(string filePath, string contentType)>> GetTaskAttachmentAsync(int attachmentId, int currentUserId)
    {
        var file = await _userTaskRepo.GetTaskAttachmentAsync(attachmentId);

        if (file == null) return Result<(string, string)>.Failure("Attachment Not Found");

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), _filesDir, currentUserId.ToString(), file.FileName);

        if (!File.Exists(filePath)) return Result<(string, string)>.Failure("Attachment Not Found");

        new FileExtensionContentTypeProvider().TryGetContentType(file.FileName, out var contentType);

        if (string.IsNullOrEmpty(contentType)) contentType = "application/octet-stream";

        return (filePath, contentType);
    }

    public Task<UserWithSharedTask?> GetTaskByIdAsync(int taskId)
    {
        return _userTaskRepo.GetTaskByIdAsync(taskId);
    }

    public async Task<Result<bool>> ShareTaskAsync(ShareTaskDto shareTaskDto, int currentUserId)
    {
        var task = await GetTaskByIdAsync(shareTaskDto.TaskId);

        if (task == null) return Result<bool>.Failure("Failed To Retrieve Task");

        if (task.CreatedBy != currentUserId)
            return Result<bool>.Failure("Not your task");

        if (shareTaskDto.SharedWith.Count == 0)
        {
            await _userTaskRepo.DeleteAllSharedWithAsync(shareTaskDto.TaskId);
            return Result<bool>.Successful(true);
        }

        var users = await _userRepo.GetUserstoShareAsync(task.CreatedBy);

        if (users.Count == 0) return Result<bool>.Failure("Failed to Retrieve Users");

        var blockedUsers = users.Where(u => !u.Status).Select(u => u.Id).ToList();

        if (task.SharedTasks.Count == 0)
        {
            shareTaskDto.SharedWith = shareTaskDto.SharedWith.Except(blockedUsers).ToList();
            await _userTaskRepo.ShareTaskAsync(shareTaskDto, task.CreatedBy);
            return Result<bool>.Successful(true);
        }

        var alreadySharedWith = task.SharedTasks.Select(a => a.SharedWith).ToList();
        var usersToDelete = alreadySharedWith.Except(shareTaskDto.SharedWith).ToList();

        if (usersToDelete.Count > 0)
            await _userTaskRepo.DeleteSharedWithAsync(usersToDelete, shareTaskDto.TaskId);

        shareTaskDto.SharedWith = shareTaskDto.SharedWith
            .Except(alreadySharedWith)
            .Except(blockedUsers)
            .ToList();

        if (shareTaskDto.SharedWith.Count > 0)
            await _userTaskRepo.ShareTaskAsync(shareTaskDto, task.CreatedBy);

        return Result<bool>.Successful(true);
    }

    public Task<List<UserTask>> GetUserTasksAsync(int userId)
    {
        return _userTaskRepo.GetUserTasksAsync(userId);
    }
}