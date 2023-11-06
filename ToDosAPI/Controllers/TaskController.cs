using Microsoft.AspNetCore.Mvc;
using ToDosAPI.Models.TaskClasses;

namespace ToDosAPI.Controllers
{
    public class TasksController : ControllerBase
    {

        private readonly ITaskServices _taskService;

        public TasksController(ITaskServices taskSer)
        {
            _taskService = taskSer;
        }

        [HttpGet]
        public async Task<List<UserTask>> GetAllTasks()
        {
            return await _taskService.GetAllTasks();
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetAllTasks(int userId)
        {
            var list = await _taskService.GetUserTasksAsync(userId);
            return list.Count > 0 ? Ok(list) : NotFound();

        }

        [HttpPost]
        public async Task<ActionResult> AddTask([FromBody] Models.TaskClasses.UserTask task)
        {
            var check = await _taskService.AddNewTask(task);
            return check ? Ok(true) : BadRequest();

        }


        [HttpPut("{taskId}")]
        public async Task<ActionResult> EditTask(int taskId, string taskContent, int status)
        {
            var check = await _taskService.EditTask(taskId, taskContent, status);
            return check ? Ok(true) : BadRequest();
        }

        [HttpDelete("{taskId}")]
        public async Task<ActionResult> DeleteTask(int taskId)
        {
            var check = await _taskService.DeleteTask(taskId);
            return check ? Ok(true) : BadRequest();
        }
    }
}
