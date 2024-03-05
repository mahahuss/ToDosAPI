using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Core.Dtos
{
    public class UpdateUserProfileDto
    {
        [Required] public string Name { get; set; } = default!;
        public IFormFile? Image { get; set; }
    }
}
