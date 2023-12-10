using System.ComponentModel.DataAnnotations;

namespace ToDosAPI.Models.Dtos
{
    public class UserTasksWithAttachs
    {
        public int Id { get; set; }
        public string TaskContent { get; set; } = default!;
        public int CreatedBy { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<string> files { get; set; } = new();
    }
}
