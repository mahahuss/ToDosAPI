namespace ToDosAPI.Models.TaskClasses
{
    public class Task
    {
        public int Id { get; set; }
        public string? TaskContent { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }

    }
}
