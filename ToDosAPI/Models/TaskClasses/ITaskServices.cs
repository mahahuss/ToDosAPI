using ToDosAPI.Models.UserClasses;

namespace ToDosAPI.Models.TaskClasses
{
    public interface ITaskServices
    {
        Task<bool> AddNewTask(Task task);
        Task<bool> EditTask(int taskId, string taskContent, int status);
        Task<bool> DeleteTask(int taskId);
        Task<List<Task>> GetAllTasks();
        Task<List<Task>> GetAllTasks(int userId);







    }
}
