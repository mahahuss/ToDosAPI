namespace ToDosAPI.Models.Entities;

public class SharedTask
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public int SharedBy { get; set; }
    public int SharedWith { get; set; }
    public bool IsEditable { get; set; }
    public DateTime SharedDate { get; set; }
}