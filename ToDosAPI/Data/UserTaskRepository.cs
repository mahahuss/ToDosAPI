using Dapper;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using ToDosAPI.Models;

namespace ToDosAPI.Data
{
    public class UserTaskRepository
    {
        private readonly DapperDbContext _context;

        public UserTaskRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<UserTask?> CreateTaskAsync(UserTask task)
        {
            await using var con = new SqlConnection(_context.connectionstring);
            return await con.QueryFirstOrDefaultAsync<UserTask>("sp_GetUser", new { task.TaskContent, task.CreatedBy, task.Status });
        }

        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            using var con = new SqlConnection(_context.connectionstring);
            return await con.ExecuteAsync("sp_DeleteTask", new { taskId }) > 0;
        }

        public async Task<bool> EditTaskAsync(int taskId, string taskContent, int status)
        {
            using var con = new SqlConnection(_context.connectionstring);
            return await con.ExecuteAsync("sp_EditTask", new { taskContent, taskId, status }) > 0;
        }

        public async Task<List<UserTask>> GetAllTasksAsync()
        {
            using var con = new SqlConnection(_context.connectionstring);
            return (await con.QueryAsync<UserTask>("sp_GetAllTasks")).ToList();
        }

        public async Task<List<UserTask>> GetUserTasksAsync(int userId)
        {
            using var con = new SqlConnection(_context.connectionstring);
            return (await con.QueryAsync<UserTask>("sp_GetUserTasks", new { userId })).ToList();
        }

    }
}
