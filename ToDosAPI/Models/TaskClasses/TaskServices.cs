using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using ToDosAPI.Models.Dapper;
using ToDosAPI.Models.UserClasses;

namespace ToDosAPI.Models.TaskClasses;

public class TaskServices : ITaskServices
{
    private readonly DapperDBContext _context;
    public TaskServices(DapperDBContext context)
    {
        _context = context;
    }
    public async Task<bool> AddNewTask(Task task)
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

    public async Task<List<Task>> GetAllTasks()
    {
        string query = "Select * From Tasks";
        using var con = new SqlConnection(_context.connectionstring);
        return (await con.QueryAsync<Task>(query)).ToList();
    }

    public async Task<List<Task>> GetAllTasks(int userId)
    {
        string query = "Select * From Tasks where CreatedBy = @userId ";
        using var con = new SqlConnection(_context.connectionstring);
        return (await con.QueryAsync<Task>(query, new { userId })).ToList();
    }
}