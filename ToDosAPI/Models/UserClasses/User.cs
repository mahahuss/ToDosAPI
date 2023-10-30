namespace ToDosAPI.Models.UserClasses
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public int UserType { get; set; }
        public string FullName { get; set; }
    }
}
