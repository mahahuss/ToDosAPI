using Dapper;
using System.Data;
using System.Threading.Tasks;
using ToDosAPI.Models.Dapper;
using ToDosAPI.Models.UserClasses;

namespace ToDosAPI.Models.TaskClasses
{
    public class TaskServices : ITaskServices
    {
        private readonly DapperDBContext context;
        public TaskServices(DapperDBContext context)
        {
            this.context = context;
        }
        public async Task<string> AddNewTask(Task task)
        {
            var response = "";

            string query = " insert into Tasks (TaskContent, CreatedDate ,CreatedBy,Status) Values (@TaskContent , GETDATE() ,@CreatedBy, @Status) ";
            var parameters = new DynamicParameters();
            parameters.Add("TaskContent", task.TaskContent, DbType.String);
            parameters.Add("CreatedBy", task.CreatedBy, DbType.Int64);
            parameters.Add("Status", task.Status, DbType.Int64);

            using (var connectin = this.context.CreateConnection())
            {
                await connectin.ExecuteAsync(query, parameters);
                response = "true";

            }
            return response;
        }

        public async Task<string> DeleteTask(int TaskId)
        {
            var response = "";

            string query = " delete from Tasks where Id = @Id ";
            var parameters = new DynamicParameters();
            parameters.Add("Id", TaskId, DbType.Int64);
            using (var connectin = this.context.CreateConnection())
            {
                await connectin.ExecuteAsync(query, parameters);
                response = "true";

            }
            return response;
        }

        public async Task<string> EditTask(int TaskId, String TaskContent)
        {
            var response = "";

            string query = " Update Tasks set TaskContent = @TaskContent where Id = @Id ";
            var parameters = new DynamicParameters();
            parameters.Add("Id", TaskId, DbType.Int64);
            parameters.Add("TaskContent", TaskContent, DbType.String);

            using (var connectin = this.context.CreateConnection())
            {
                await connectin.ExecuteAsync(query, parameters);
                response = "true";

            }
            return response;
        }

        public async Task<List<Task>> GetAllTasks()
        {
            string query = "Select * From Tasks";
          
            using (var connectin = this.context.CreateConnection())
            {
                var Taskslist = await connectin.QueryAsync<Task>(query);
                return Taskslist.ToList();
            }
        }

        public async Task<List<Task>> GetAllTasks(int UserId)
        {
            string query = "Select * From Tasks where CreatedBy = @CreatedBy ";
            var parameters = new DynamicParameters();
            parameters.Add("CreatedBy", UserId, DbType.Int64);
            using (var connectin = this.context.CreateConnection())
            {
                var Taskslist = await connectin.QueryAsync<Task>(query,parameters);
                return Taskslist.ToList();
            }
        }

        public async Task<string> UpdateTaskStatus(int TaskId, int Status)
        {
            var response = "";

            string query = " Update Tasks set Status = @Status where Id = @Id ";
            var parameters = new DynamicParameters();
            parameters.Add("Id", TaskId, DbType.Int64);
            parameters.Add("Status", Status, DbType.Int64);

            using (var connectin = this.context.CreateConnection())
            {
                await connectin.ExecuteAsync(query, parameters);
                response = "true";

            }
            return response;
        }
    }
}
