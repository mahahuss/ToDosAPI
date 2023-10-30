using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using ToDosAPI.Models.TaskClasses;
using ToDosAPI.Models.UserClasses;

namespace ToDosAPI.Controllers
{
    public class TasksController : Controller
    {

        private readonly ITaskServices TaskSer;

        public TasksController(ITaskServices TaskSer)
        {
            this.TaskSer = TaskSer;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var _list = await TaskSer.GetAllTasks();
            if (_list != null)
            {
                return Ok(_list);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAllTasks(int userId)
        {
            var _list = await TaskSer.GetAllTasks(userId);
            if (_list != null)
            {
                return Ok(_list);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] Models.TaskClasses.Task task)
        {
            var Check = await TaskSer.AddNewTask(task);
            if (Check.ToString() != "")
                return Ok(true);
            else
                return BadRequest();

        }


        [HttpPut("{taskId}")]
        public async Task<IActionResult> EditTask(int taskId, string TaskContent)
        {
            var Check = await TaskSer.EditTask(taskId, TaskContent);
            if (Check.ToString() != "")
                return Ok(true);
            else
                return BadRequest();
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            var check = await TaskSer.DeleteTask(taskId);
            return check ? Ok(true) : BadRequest();
        }
    }
}
