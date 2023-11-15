namespace ToDosAPI.Models
{
    public class UserTask
    {
        public int Id { get; set; }
        public string? TaskContent { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int Status { get; set; }

    }
}
