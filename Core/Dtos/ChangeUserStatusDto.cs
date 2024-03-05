namespace Core.Dtos
{
    public class ChangeUserStatusDto
    {
        public int userId { get; set; } = default!;
        public bool status { get; set; } = default!;
    }
}
