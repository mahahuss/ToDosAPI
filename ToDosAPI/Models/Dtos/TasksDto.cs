namespace ToDosAPI.Models.Dtos
{
    public class TasksDto
    {
        public int Id { get; set; }
        public string TaskContent { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public bool Status { get; set; }
    }
}
