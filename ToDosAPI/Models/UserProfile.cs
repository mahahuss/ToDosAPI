using System.ComponentModel.DataAnnotations;

namespace ToDosAPI.Models
{
    public class UserProfile
    {
        [Required] public string Name { get; set; } = default!;
        public IFormFile? Image { get; set; }
    }
}
