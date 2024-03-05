using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class DeleteTaskDto
    {
        [Required] public int TaskId { get; set; } = default!;

    }
}
