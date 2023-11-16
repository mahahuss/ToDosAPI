using System.ComponentModel.DataAnnotations;

namespace ToDosAPI.Models.Dtos
{
    public class DeleteTaskDto
    {
        [Required] public int TaskId { get; set; } = default!;

    }
}
