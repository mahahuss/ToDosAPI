namespace ToDosAPI.Models.Dtos
{
    public class UserWithRolesDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public string? FullName { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
