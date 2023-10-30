using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using ToDosAPI.Models.TaskClasses;
using ToDosAPI.Models.UserClasses;

namespace ToDosAPI.Controllers
{
    public class TaskController : Controller
    {

        private readonly ITaskServices TaskSer;
        public TaskController(ITaskServices TaskSer)
        {
            this.TaskSer = TaskSer;
        }

        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var _list = await this.TaskSer.GetAllTasks();
            if (_list != null)
            {
                return Ok(_list);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("GetAllTasks/{UserId}")]
        public async Task<IActionResult> GetAllTasks(int UserId)
        {
            var _list = await this.TaskSer.GetAllTasks(UserId);
            if (_list != null)
            {
                return Ok(_list);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask([FromBody] Models.TaskClasses.Task task)
        {
            var Check = TaskSer.AddNewTask(task);
            if (Check.ToString() != "")
                return Ok(true);
            else
                return BadRequest();

        }


        [HttpPut("EditTask")]
        public async Task<IActionResult> EditTask(int TaskId, String TaskContent)
        {
            var Check = TaskSer.EditTask(TaskId,TaskContent);
            if (Check.ToString() != "")
                return Ok(true);
            else
                return BadRequest();

        }

        [HttpPut("UpdateTaskStatus")]
        public async Task<IActionResult> UpdateTaskStatus(int TaskId, int Status)
        {
            var Check = TaskSer.UpdateTaskStatus(TaskId, Status);
            if (Check.ToString() != "")
                return Ok(true);
            else
                return BadRequest();

        }

        [HttpDelete("DeleteTask")]
        public async Task<IActionResult> DeleteTask(int TaskId)
        {
            var Check = TaskSer.DeleteTask(TaskId);
            if (Check.ToString() != "")
                return Ok(true);
            else
                return BadRequest();

        }
    }
}
