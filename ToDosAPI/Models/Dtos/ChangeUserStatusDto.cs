namespace ToDosAPI.Models.Dtos
{
    public class ChangeUserStatusDto
    {
        public int userId { get; set; } = default!;
        public string status { get; set; } = default!;
    }
}
