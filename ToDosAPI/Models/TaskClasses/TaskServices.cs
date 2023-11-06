using Dapper;
using Microsoft.Data.SqlClient;
using ToDosAPI.Data;

namespace ToDosAPI.Models.TaskClasses;

public class TaskServices : ITaskServices
{
    private readonly DapperDbContext _context;
    public TaskServices(DapperDbContext context)
    {
        _context = context;
    }
    public async Task<bool> AddNewTask(UserTask task)
    {
        string query = " insert into Tasks (TaskContent, CreatedDate ,CreatedBy,Status)";
        query += " Values (@TaskContent , GETDATE() ,@CreatedBy, @Status) ";
        using var con = new SqlConnection(_context.connectionstring);
        return await con.ExecuteAsync(query, new { task.TaskContent, task.CreatedBy, task.Status }) > 0;
    }

    public async Task<bool> DeleteTask(int taskId)
    {
        string query = " delete from Tasks where Id = @taskId ";
        using var con = new SqlConnection(_context.connectionstring);
        return await con.ExecuteAsync(query, new { taskId }) > 0;

    }

    public async Task<bool> EditTask(int taskId, String taskContent, int status)
    {
        string query = " Update Tasks set TaskContent = @taskContent, Status = @status where Id = @taskId ";
        using var con = new SqlConnection(_context.connectionstring);
        return await con.ExecuteAsync(query, new { taskContent, taskId, status }) > 0;

    }

    public async Task<List<UserTask>> GetAllTasks()
    {
        string query = "Select * From Tasks";
        using var con = new SqlConnection(_context.connectionstring);
        return (await con.QueryAsync<UserTask>(query)).ToList();
    }

    public async Task<List<UserTask>> GetUserTasksAsync(int userId)
    {
        string query = "Select * From Tasks where CreatedBy = @userId ";
        using var con = new SqlConnection(_context.connectionstring);
        return (await con.QueryAsync<UserTask>(query, new { userId })).ToList();
    }
}