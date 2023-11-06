namespace ToDosAPI.Models.UserClasses
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Salt { get; set; } = default!;
        public int UserType { get; set; }
        public string? FullName { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
