using ToDosAPI.Models.UserClasses;

namespace ToDosAPI.Models.TaskClasses
{
    public interface ITaskServices
    {
        Task<bool> AddNewTask(Task task);
        Task<bool> EditTask(int TaskId, string TaskContent);
        Task<bool> UpdateTaskStatus(int TaskId, int Status);
        Task<bool> DeleteTask(int TaskId);
        Task<List<Task>> GetAllTasks();
        Task<List<Task>> GetAllTasks(int UserId);







    }
}
