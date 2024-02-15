using System.ComponentModel.DataAnnotations;
using ToDosAPI.Models.Entities;

namespace ToDosAPI.Models.Dtos;

public class EditTaskDto
{
    [Required] public int Id { get; set; }
    [Required] public int CreatedBy { get; set; }
    [Required] public string TaskContent { get; set; } = default!;
    [Required] public bool Status { get; set; }
    public List<TaskAttachment> Files { get; set; } = [];
}

public class EditTaskFormDto
{
    [Required] public string TaskJson { get; set; } = default!;
    public List<IFormFile> Files { get; set; } = [];
}