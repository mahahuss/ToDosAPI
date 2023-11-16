using System.ComponentModel.DataAnnotations;

namespace ToDosAPI.Models.Dtos
{
    public class EditTaskDto
    {
        [Required] public int TaskId { get; set; } = default!;
        [Required] public string TaskContent { get; set; } = default!;
        [Required] public int Status { get; set; } = default!;
    }
}
