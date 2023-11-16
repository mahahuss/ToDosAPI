using Dapper;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Models.Entities;

namespace ToDosAPI.Data
{
    public class UserTaskRepository
    {
        private readonly DapperDbContext _context;

        public UserTaskRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<UserTask?> CreateTaskAsync(AddTaskDto task)
        {
            await using var con = new SqlConnection(_context.connectionstring);
            return await con.QueryFirstOrDefaultAsync<UserTask>("sp_TaskCreate", new { task.TaskContent, task.CreatedBy, task.Status });
        }

        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            await using var con = new SqlConnection(_context.connectionstring);
            return await con.ExecuteAsync("sp_TaskDelete", new { taskId }) > 0;
        }

        public async Task<bool> EditTaskAsync(int taskId, string taskContent, int status)
        {
            await using var con = new SqlConnection(_context.connectionstring);
            return await con.ExecuteAsync("sp_TaskEdit", new { taskContent, taskId, status }) > 0;
        }

        public async Task<List<UserTask>> GetAllTasksAsync()
        {
            await using var con = new SqlConnection(_context.connectionstring);
            return (await con.QueryAsync<UserTask>("sp_TaskGetAll")).ToList();
        }

        public async Task<List<UserTask>> GetUserTasksAsync(int userId)
        {
            await using var con = new SqlConnection(_context.connectionstring);
            return (await con.QueryAsync<UserTask>("sp_TaskGetUserTasks", new { userId })).ToList();
        }

    }
}
