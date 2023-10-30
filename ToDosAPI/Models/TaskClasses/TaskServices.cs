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
        string query = " insert into Tasks (TaskContent, CreatedDate ,CreatedBy,Status) Values (@TaskContent , GETDATE() ,@CreatedBy, @Status) ";
        await using var con = new SqlConnection(_context.connectionstring);
        return await con.ExecuteAsync(query, new { task.TaskContent, task.CreatedBy, task.Status }) > 0;
    }

    public async Task<bool> DeleteTask(int TaskId)
    {
        string query = " delete from Tasks where Id = @Id ";
        var parameters = new DynamicParameters();
        parameters.Add("Id", TaskId, DbType.Int64);
        await using var con = new SqlConnection(_context.connectionstring);

        return await con.ExecuteAsync(query, parameters) > 0;

    }

    public async Task<bool> EditTask(int TaskId, String TaskContent)
    {
        string query = " Update Tasks set TaskContent = @TaskContent where Id = @Id ";
        var parameters = new DynamicParameters();
        parameters.Add("Id", TaskId, DbType.Int64);
        parameters.Add("TaskContent", TaskContent, DbType.String);

        await using var con = new SqlConnection(_context.connectionstring);
        return await con.ExecuteAsync(query, parameters) > 0;
    }

    public async Task<List<Task>> GetAllTasks()
    {
        string query = "Select * From Tasks";

        await using var con = new SqlConnection(_context.connectionstring);
        var Taskslist = await con.QueryAsync<Task>(query);
        return Taskslist.ToList();
    }

    public async Task<List<Task>> GetAllTasks(int userId)
    {
        await using var con = new SqlConnection(_context.connectionstring);
        return (await con.QueryAsync<Task>("sp_TasksGetAll", new { userId })).ToList();
    }

    public async Task<bool> UpdateTaskStatus(int TaskId, int Status)
    {
        string query = " Update Tasks set Status = @Status where Id = @Id ";
        var parameters = new DynamicParameters();
        parameters.Add("Id", TaskId, DbType.Int64);
        parameters.Add("Status", Status, DbType.Int64);

        await using var con = new SqlConnection(_context.connectionstring);

        return await con.ExecuteAsync(query, parameters) > 0;

    }
}