using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using ToDosAPI.Models.TaskClasses;
using ToDosAPI.Models.UserClasses;

namespace ToDosAPI.Controllers
{
    public class TasksController : Controller
    {

        private readonly ITaskServices _taskService;

        public TasksController(ITaskServices taskSer)
        {
            this._taskService = taskSer;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var _list = await _taskService.GetAllTasks();
            return _list != null ? Ok(_list) : NotFound();

        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllTasks(int userId)
        {
            var _list = await _taskService.GetAllTasks(userId);
            return _list !=null? Ok(_list) : NotFound();
           
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] Models.TaskClasses.Task task)
        {
            var check = await _taskService.AddNewTask(task);
            return check ? Ok(true) : BadRequest();

        }


        [HttpPut("{taskId}")]
        public async Task<IActionResult> EditTask(int taskId, string taskContent, int status)
        {
            var check = await _taskService.EditTask(taskId, taskContent, status);
            return check ? Ok(true) : BadRequest();
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            var check = await _taskService.DeleteTask(taskId);
            return check ? Ok(true) : BadRequest();
        }
    }
}
