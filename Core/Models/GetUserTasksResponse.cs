using Core.Entities;

namespace Core.Models
{
    public class GetUserTasksResponse
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public List<UserWithSharedTask> Tasks { get; set; } = [];
    }
}