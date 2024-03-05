using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dtos
{
    public class DeleteTaskDto
    {
        [Required] public int TaskId { get; set; } = default!;

    }
}
