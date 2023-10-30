using ToDosAPI.Models.UserClasses;

namespace ToDosAPI.Models.TaskClasses
{
    public interface ITaskServices
    {
        Task<String> AddNewTask(Task task);
        Task<String> EditTask(int TaskId, String TaskContent);
        Task<String> UpdateTaskStatus(int TaskId, int Status);
        Task<String> DeleteTask(int TaskId);
        Task <List<Task>> GetAllTasks();
        Task<List<Task>> GetAllTasks(int UserId);







    }
}
