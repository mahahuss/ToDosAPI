using ToDosAPI.Models.Entities;

namespace ToDosAPI.Models;

public class GetUsers
{
    public int Id { get; set; }
    public string Username { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public bool Status { get; set; }
    public int TotalTasks { get; set; }

}

public class GetUsersWithRoles : GetUsers
{
    public Dictionary<int, string> Roles { get; set; } = new();

}