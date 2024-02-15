using ToDosAPI.Models.Dtos;

namespace ToDosAPI.Models.Entities
{
    public class SharedTask
    {
        public int Id { get; set; } = default!;
        public int TaskId { get; set; } = default!;
        public int SharedBy { get; set; } = default!;        
        public List<int> SharedWith { get; set; } = new ();
        public bool IsEditable  { get; set; } = default!;
        public DateTime SharedDate { get; set; } = default!;

    }
}
