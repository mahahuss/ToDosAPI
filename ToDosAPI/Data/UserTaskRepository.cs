using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
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
       var userTask =  await con.QueryFirstOrDefaultAsync<UserTask>("sp_TaskCreate",
            new { task.TaskContent, task.CreatedBy, task.Status });

        if (userTask != null)
        {
            foreach (var file in task.files)
            {
                await con.QueryFirstOrDefaultAsync<UserTask>("sp_TasksAttachsAddFiles",
               new { userTask.Id, file.FileName }); //check extension
            }
        }
        return userTask;
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

    public async Task<List<UserTasksWithAttachs>> GetUserTasksAsync(int userId)
    {
        await using var con = new SqlConnection(_context.ConnectionString);

        //UserTasksWithAttachs? userTasksWithAttachs = null;
        //return (await con.QueryAsync<UserTask, string, UserTasksWithAttachs>("sp_TaskGetUserTasks",
        //     (task, files) =>
        //     {
        //         userTasksWithAttachs ??= new UserTasksWithAttachs
        //         {
        //             Id = task.Id,
        //             CreatedBy = task.CreatedBy,
        //             Status = task.Status,
        //             TaskContent = task.TaskContent!,
        //             CreatedDate = task.CreatedDate,
        //         };

        //         userTasksWithAttachs.files.Add(files);
        //         return userTasksWithAttachs;

        //     }, new { userId }, splitOn: "Id, FileName")).ToList();


        List<UserTasksWithAttachs> list = new List<UserTasksWithAttachs>();


        return (await con.QueryAsync<UserTasksWithAttachs, string, UserTasksWithAttachs>("sp_TaskGetUserTasks",
              (task, file) =>
              {

                  var check = list.Any((element) =>
                  {
                      return element.Id == task.Id;
                  });

                  if (!check)
                  {
                      task.files.Add(file);
                      list.Append(task);
                  }

                  else
                      list.Last().files.Add(file);

                  return task;

              }, new { userId })).ToList();

        //return list;

        



        //return (await con.QueryAsync<UserTasksWithAttachs, string, UserTasksWithAttachs>("sp_TaskGetUserTasks",
        //    (task, file) =>
        //    {
        //        task.files.Add(file);+

        //        return task;

        //    }, new { userId }, splitOn: "FileName")).ToList();
    }
}