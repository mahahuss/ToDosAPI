namespace ToDosAPI.Models.Entities;

public class UserTask
{
    public int Id { get; set; }
    public string TaskContent { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public int CreatedBy { get; set; }
    public bool Status { get; set; }
    public List<TaskAttachment> Files { get; set; } = new();
}

public class UserWithSharedTask : UserTask
{
    // public List<SharedTask> SharedTasks { get; set; } = new();

    public bool? IsEditable { get; set; }
    public string? SharedBy { get; set; }

}