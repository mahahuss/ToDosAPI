using System.ComponentModel.DataAnnotations;

namespace Core.Dtos;

public class LoginDto
{
    [Required] public string Username { get; set; } = default!;
    [Required] public string Password { get; set; } = default!;
}