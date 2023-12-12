namespace ToDosAPI.Models.Entities;

public class TaskAttachment
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public string FileName { get; set; } = default!;
}