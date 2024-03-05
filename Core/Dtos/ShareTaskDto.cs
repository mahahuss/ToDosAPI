namespace Core.Dtos
{
    public class ShareTaskDto
    {
        public int TaskId { get; set; } = default!;
        public bool IsEditable { get; set; } = default!;
        public List<int> SharedWith { get; set; } = [];
    }
}