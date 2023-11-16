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
        return await con.QueryFirstOrDefaultAsync<User>("sp_UserCreate", new
        {
            user.Username,
            user.Password,
            user.Salt,
            user.FullName,
            roleId
        });
    }

    public async Task<UserWithRolesDto?> GetUserWithRolesAsync(string username, string type)
    {
        await using var con = new SqlConnection(_context.connectionstring);
        UserWithRolesDto? userWithRoles = null;
        await con.QueryAsync<User, Role, UserWithRolesDto>("sp_UserGetUserWithRoles",
            (user, role) =>
            {
                userWithRoles ??= new UserWithRolesDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Username = user.Username,
                    Password = type == "register" ? null : user.Password,
                    Salt = type == "register" ? null : user.Salt
                };

                userWithRoles.Roles.Add(role.UserType);
                return userWithRoles;
            }, new { username });

        return userWithRoles;
    }

    public async Task<UserWithRolesDto?> GetUserWithRolesAsync(string username)
    {
        await using var con = new SqlConnection(_context.connectionstring);
        UserWithRolesDto? userWithRoles = null;
        await con.QueryAsync<User, Role, UserWithRolesDto>("sp_UserGetUserWithRoles",
            (user, role) =>
            {
                userWithRoles ??= new UserWithRolesDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Username = user.Username,
                    Password =  user.Password,
                    Salt = user.Salt
                };

                userWithRoles.Roles.Add(role.UserType);
                return userWithRoles;
            }, new { username });

        return userWithRoles;
    }
}