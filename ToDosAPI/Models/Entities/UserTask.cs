using ToDosAPI.Models.Dtos;

namespace ToDosAPI.Models.Entities;

public class UserTask
{
    public int Id { get; set; }
    public string TaskContent { get; set; }
    public DateTime CreatedDate { get; set; }
    public int CreatedBy { get; set; }
    public bool Status { get; set; }
    public List<TasksAttachmentsDto> Files { get; set; } = new();

}