using System.ComponentModel.DataAnnotations;

namespace ToDosAPI.Models
{
    public class UpdateUserProfileDto
    {
        [Required] public string Name { get; set; } = default!;
        public IFormFile? Image { get; set; }
    }
}
