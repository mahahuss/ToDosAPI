using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using ToDosAPI.Models;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Models.Entities;

namespace ToDosAPI.Data;

public class UserTaskRepository
{
    private readonly DapperDbContext _context;

    public UserTaskRepository(DapperDbContext context)
    {
        _context = context;
    }

    public async Task<UserTask?> CreateTaskAsync(AddTaskDto task)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        var userTask = await con.QueryFirstOrDefaultAsync<UserTask>("sp_TasksCreate",
            new { task.TaskContent, task.CreatedBy, task.Status });
        return userTask;
    }

    public async Task<TaskAttachment> CreateTaskAttachmentAsync(int taskId, string fileName)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        var taskFile = await con.QuerySingleAsync<TaskAttachment>("sp_TasksAttachmentsAddFiles",
            new { Id = taskId, fileName });
        return taskFile;
    }

    public async Task<bool> DeleteTaskAsync(int taskId)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        return await con.ExecuteAsync("sp_TasksDelete", new { taskId }) > 0;
    }

    public async Task<bool> EditTaskAsync(EditTaskDto editTaskDto)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        return await con.ExecuteAsync("sp_TasksEdit",
            new { Id = editTaskDto.Id, status = editTaskDto.Status, taskContent = editTaskDto.TaskContent }) > 0;
    }

    public async Task<List<UserTask>> GetAllTasksAsync()
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        return (await con.QueryAsync<UserTask>("sp_TasksGetAll")).ToList();
    }

    public async Task<GetUserTasksResponse> GetUserTasksAsync(int userId, int pageNumber, int pageSize)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        var fromIndex = (pageNumber - 1) * pageSize;
        await using var reader =
            await con.QueryMultipleAsync("sp_TasksGetUserTasks", new { userId, fromIndex, toIndex = pageSize });
        //1- get tasks count
        var totalTasks = await reader.ReadSingleAsync<int>();

        //assign values
        var response = new GetUserTasksResponse
        {
            PageSize = pageSize,
            PageNumber = pageNumber
        };

        //2- check
        if (totalTasks == 0) return response;

        var totalPages = (int)Math.Ceiling((double)totalTasks / pageSize);
        response.TotalPages = totalPages;
        var tasks = new Dictionary<int, UserWithSharedTask>();

        //get tasks
        reader.Read<UserWithSharedTask, SharedTask?, TaskAttachment?, UserWithSharedTask>((task, sharedWith, file) =>
        {
            if (!tasks.TryGetValue(task.Id, out var taskInDictionary))
            {
                if (file is not null)
                    task.Files.Add(file);
                if (sharedWith is not null)
                    task.SharedTasks.Add(sharedWith);
                tasks.Add(task.Id, task);
            }
            else
            {
                if (file is not null)
                {
                    var fileAlreadyExist = taskInDictionary.Files.Where(x => x.Id == file.Id);
                    if (fileAlreadyExist.Count() == 0)
                        taskInDictionary.Files.Add(file);
                }
                if (sharedWith is not null)
                    taskInDictionary.SharedTasks.Add(sharedWith);
            }

            return task;
        });

        response.Tasks = tasks.Values.ToList();
        return response;
    }

    public async Task<TaskAttachment?> GetTaskAttachmentAsync(int userId)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        var attachment = await con.QueryFirstOrDefaultAsync<TaskAttachment>("sp_TasksAttachmentsGet", new { Id = userId });
        return attachment;
    }

    public async Task<UserWithSharedTask?> GetTaskByIdAsync(int taskId)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        UserWithSharedTask? taskWithAttachments = null;


        await con.QueryAsync<UserWithSharedTask, SharedTask?, TaskAttachment?, UserWithSharedTask>("sp_TasksGetById",
    (task, sharedWith, file) =>
    {


        taskWithAttachments ??= new UserWithSharedTask
        {
            Id = task.Id,
            CreatedBy = task.CreatedBy,
            CreatedDate = task.CreatedDate,
            Status = task.Status,
            TaskContent = task.TaskContent,
        };

        if (file is not null)
            taskWithAttachments.Files.Add(file);
        if (sharedWith is not null)
            taskWithAttachments.SharedTasks.Add(sharedWith);

        return taskWithAttachments;
    }, new { Id = taskId });
        return taskWithAttachments;
    }

    public async Task ShareTaskAsync(ShareTaskDto shareTaskDto, int userId)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        await con.OpenAsync();
        await using var tran = await con.BeginTransactionAsync();
        await con.ExecuteAsync("sp_SharedTasksDeleteSharedWith", new { taskId = shareTaskDto.TaskId }, transaction: tran);
        foreach (var shareWith in shareTaskDto.SharedWith)
        {
            await con.ExecuteAsync("sp_TasksShare", new
            {
                userToShare = shareWith,
                shareTaskDto.TaskId,
                shareTaskDto.IsEditable,
                userId
            }, transaction: tran);
        }

        await tran.CommitAsync();

    }

    public async Task<List<UserTask>> GetUserTasksAsync(int userId)
    {
        await using var con = new SqlConnection(_context.ConnectionString);

        var tasks = new Dictionary<int, UserTask>();

        await con.QueryAsync<UserTask, TaskAttachment?, UserTask>("sp_TasksGetUserTasksOnly",
            (task, file) =>
            {
                if (!tasks.TryGetValue(task.Id, out var taskInDictionary))
                {
                    if (file is not null)
                        task.Files.Add(file);
                    tasks.Add(task.Id, task);
                }
                else if (file is not null)
                    taskInDictionary.Files.Add(file);
                
                return task;
            }, new { userId });

        return tasks.Values.ToList();
    }

    public async Task<bool> DeleteFileAsync(int attachmentId)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        return await con.ExecuteAsync("sp_TasksAttachmentsDelete", new { Id = attachmentId }) > 0;
    }
}