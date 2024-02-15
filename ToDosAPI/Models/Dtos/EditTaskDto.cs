using System.ComponentModel.DataAnnotations;

namespace ToDosAPI.Models.Dtos;

public class EditTaskDto
{
    [Required] public int Id { get; set; }
    [Required] public int CreatedBy { get; set; }
    [Required] public string TaskContent { get; set; } = default!;
    [Required] public bool Status { get; set; }
    public List<IFormFile> Files { get; set; } = new();

}