using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Core.Dtos
{
    public class AddTaskDto
    {
        [Required] public string TaskContent { get; set; } = default!;
        [Required] public int CreatedBy { get; set; }
        [Required] public bool Status { get; set; }
        public List<IFormFile> Files { get; set; } = new();
    }
}