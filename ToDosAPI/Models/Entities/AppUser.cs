namespace ToDosAPI.Models.Entities;

public class AppUser
{
    public int Id { get; set; }
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Salt { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string Status { get; set; } = default!;

}