using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dtos
{
    public class UpdateUserProfileDto
    {
        [Required] public string Name { get; set; } = default!;
        public IFormFile? Image { get; set; }
    }
}
