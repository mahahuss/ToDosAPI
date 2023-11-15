using Dapper;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using ToDosAPI.Models.Dtos;
using ToDosAPI.Models.Entities;

namespace ToDosAPI.Data;

public class UserRepository
{
    private readonly DapperDbContext _context;

    public UserRepository(DapperDbContext context)
    {
        _context = context;
    }

    public async Task<User?> CreateUserAsync(User user, int roleId)
    {
        await using var con = new SqlConnection(_context.connectionstring);
        return await con.QueryFirstOrDefaultAsync<User>("sp_CreateUser", new
        {
            user.Username,
            user.Password,
            user.Salt,
            user.FullName,
            roleId
        });
    }

    public async Task<User?> GetUserAsync(string username)
    {
        using var con = new SqlConnection(_context.connectionstring);
        return await con.QueryFirstOrDefaultAsync<User>("sp_GetUser", new { username });
    }

    public async Task<UserWithRolesDto?> GetUsersWithRolesAsync(string username)
    {
        await using var con = new SqlConnection(_context.connectionstring);
        UserWithRolesDto? userWithRoles = null;
        await con.QueryAsync<User, Role, UserWithRolesDto>("sp_GetUsersWithRoles",
            (user, role) =>
            {
                userWithRoles ??= new UserWithRolesDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Username = user.Username,
                };

                userWithRoles.Roles.Add(role.UserType);
                return userWithRoles;
            }, new { username });

        return userWithRoles;
    }
}