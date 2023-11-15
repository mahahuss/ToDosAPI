using Microsoft.AspNetCore.Mvc;
using ToDosAPI.Models;
using ToDosAPI.Services;

namespace ToDosAPI.Controllers
{
    public class TasksController : ControllerBase
    {

        private readonly TaskServices _taskService;

        public TasksController(TaskServices taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTasks()
        {
            List<UserTask> tasks = await _taskService.GetAllTasks();
            return Ok(tasks);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetAllTasks(int userId)
        {
            List<UserTask> tasks = await _taskService.GetUserTasks(userId);
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<ActionResult> AddTask([FromBody] UserTask task)
        {
            var userTask = await _taskService.AddNewTask(task);
            return Ok(userTask);
        }


        [HttpPut]
        public async Task<ActionResult> EditTask(int taskId, string taskContent, int status)
        {
            var check = await _taskService.EditTask(taskId, taskContent, status);
            return Ok(check);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteTask(int taskId)
        {
            var check = await _taskService.DeleteTask(taskId);
            return Ok(check);
        }
    }
}
