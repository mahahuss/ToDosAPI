using System.ComponentModel.DataAnnotations;

namespace ToDosAPI.Models.Dtos;

public class LoginDto
{
    [Required] public string Username { get; set; } = default!;
    [Required] public string Password { get; set; } = default!;
}