using Dapper;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using ToDosAPI.Models;

namespace ToDosAPI.Data;

public class UserRepository
{
    private readonly DapperDbContext _context;

    public UserRepository(DapperDbContext context)
    {
        _context = context;
    }

    public async Task<User?> CreateUserAsync(User user)
    {
        using var con = new SqlConnection(_context.connectionstring);
        return await con.QueryFirstOrDefaultAsync<User>("sp_CreateUser", new { user.Username, user.Password, user.Salt, user.FullName });
    }

    public async Task<User?> GetUserAsync(string username)
    {
        using var con = new SqlConnection(_context.connectionstring);
        return await con.QueryFirstOrDefaultAsync<User>("sp_GetUser", new { username });
    }

    public async void GetUsersWithRolesAsync(string username)
    {
         using var con = new SqlConnection(_context.connectionstring);
        var userInfo = await con.QueryAsync<UserRoles, Role, UserRoles>("sp_GetUsersWithRoles", (user, role) =>
        {
             user.Roles = user.Roles ?? new List<Role>();
             user.Roles.Add(role);
         },splitOn: "Id").Distinct().ToList();

        return userInfo;
    }
}