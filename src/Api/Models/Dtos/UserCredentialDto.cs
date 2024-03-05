using System.ComponentModel.DataAnnotations;

namespace Api.Models.Dtos
{
    public class UserCredentialDto
    {
        public string Password { get; set; } = default!;
        public string Salt { get; set; } = default!;
    }
}
