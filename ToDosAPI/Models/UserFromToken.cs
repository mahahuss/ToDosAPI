namespace ToDosAPI.Models
{
    public class GetUsers
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public bool Status { get; set; } = default!;
        public int TotalTasks { get; set; }

    }
}
