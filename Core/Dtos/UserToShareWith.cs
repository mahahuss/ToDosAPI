namespace Core.Dtos
{
    public class UserToShareWith
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string FullName { get; set; } = default!;
    }
}
