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
        var userTaskWithFiles = new UserTask();
        await using var con = new SqlConnection(_context.ConnectionString);
        var userTask = await con.QueryFirstOrDefaultAsync<TasksDto>("sp_TaskCreate",
             new { task.TaskContent, task.CreatedBy, task.Status });

        if (userTask != null)
        {
            userTaskWithFiles.Id = userTask.Id;
            userTaskWithFiles.Status = userTask.Status;
            userTaskWithFiles.CreatedDate = userTask.CreatedDate;
            userTaskWithFiles.TaskContent = userTask.TaskContent;
            userTaskWithFiles.CreatedBy = userTask.CreatedBy;

            foreach (var file in task.Files)
            {
                var taskFile = await con.QueryFirstOrDefaultAsync<TasksAttachmentsDto>("sp_TasksAttachmentsAddFiles",
                new { userTask.Id, file.FileName });
                if (taskFile != null)
                    userTaskWithFiles.Files.Add(taskFile!); 
            }
        }
        return userTaskWithFiles.TaskContent == null ? null: userTaskWithFiles;
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

    public async Task<List<UserTask>> GetUserTasksAsync(int userId)
    {
        await using var con = new SqlConnection(_context.ConnectionString);

        Dictionary<int, UserTask> tasks = new Dictionary<int, UserTask>();

        await con.QueryAsync<UserTask, TasksAttachmentsDto, UserTask>("sp_TaskGetUserTasks",
              (task, file) =>
              {
                  if (!tasks.TryGetValue(task.Id, out var taskInDictionary))
                  {
                      task.Files.Add(file);
                      tasks.Add(task.Id,task);
                  }

                  else
                      taskInDictionary.Files.Add(file);

                  return task;

              }, new { userId });

        return tasks.Values.ToList();
    }
}