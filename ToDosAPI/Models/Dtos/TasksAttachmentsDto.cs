namespace ToDosAPI.Models.Dtos
{
    public class TasksAttachmentsDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string FileName { get; set; } = default!;

    }
}
