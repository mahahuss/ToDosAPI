namespace ToDosAPI.Models
{
    public class UserFromToken
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string FullName { get; set; } = default!;
    }
}
