namespace ToDosAPI.Models.Dtos;

public class UserWithPasswordAndRolesDto
{
    public int Id { get; set; }
    public string Username { get; set; } = default!;
    public string? Password { get; set; }
    public string? Salt { get; set; }
    public string? FullName { get; set; }
    public List<string> Roles { get; set; } = new();
}

public class UserWithRolesDto
{
    public required int Id { get; set; }
    public required string Username { get; set; }
    public required string FullName { get; set; }
    public List<string> Roles { get; set; } = new();
}