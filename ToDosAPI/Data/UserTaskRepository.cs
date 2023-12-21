using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        var userTask = await con.QueryFirstOrDefaultAsync<UserTask>("sp_TaskCreate",
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
        return await con.ExecuteAsync("sp_TaskDelete", new { taskId }) > 0;
    }

    public async Task<bool> EditTaskAsync(EditTaskDto editTaskDto)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        return await con.ExecuteAsync("sp_TaskEdit", editTaskDto) > 0;
    }

    public async Task<List<UserTask>> GetAllTasksAsync()
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        return (await con.QueryAsync<UserTask>("sp_TaskGetAll")).ToList();
    }

    public async Task<List<UserWithSharedTask>> GetUserTasksAsync(int userId)
    {
        await using var con = new SqlConnection(_context.ConnectionString);

        var tasks = new Dictionary<int, UserWithSharedTask>();

        await con.QueryAsync<UserWithSharedTask, TaskAttachment?, UserWithSharedTask>("sp_TaskGetUserTasks",
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

    public async Task<TaskAttachment?> GetTaskAttachmentAsync(int userId)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        var attachment =  await con.QueryFirstOrDefaultAsync<TaskAttachment>("sp_TasksAttachmentsGet", new { Id = userId });
         return attachment;

    }

    public async Task<TasksDto?> GetTaskByIdAsync(int taskId)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        var userTask = await con.QueryFirstOrDefaultAsync<TasksDto>("sp_TasksGetById",
            new { Id = taskId });
        return userTask;
    }

    public async Task<bool> ShareTaskAsync(int userId, int taskId, bool isEditable)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        return await con.ExecuteAsync("sp_TasksAttachmentsAddFiles", new { userId, taskId, isEditable }) > 0;
    
    }
}