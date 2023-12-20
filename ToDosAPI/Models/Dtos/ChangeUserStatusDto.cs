namespace ToDosAPI.Models.Dtos
{
    public class ChangeUserStatusDto
    {
        public int userId { get; set; } = default!;
        public bool status { get; set; } = default!;
    }
}
