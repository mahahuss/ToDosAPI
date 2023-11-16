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

    public async Task<AppUser?> CreateUserAsync(AppUser appUser, int roleId)
    {
        await using var con = new SqlConnection(_context.connectionstring);
        return await con.QueryFirstOrDefaultAsync<AppUser>("sp_UserCreate", new
        {
            appUser.Username,
            appUser.Password,
            appUser.Salt,
            appUser.FullName,
            roleId
        });
    }

    public async Task<UserWithRolesDto?> GetUserWithRolesAsync(string username)
    {
        await using var con = new SqlConnection(_context.connectionstring);
        UserWithRolesDto? userWithRoles = null;
        await con.QueryAsync<AppUser, Role, UserWithRolesDto>("sp_UserGetUserWithRoles",
            (user, role) =>
            {
                userWithRoles ??= new UserWithRolesDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Username = user.Username
                };

                userWithRoles.Roles.Add(role.UserType);
                return userWithRoles;
            }, new { username });

        return userWithRoles;
    }


    

        public async Task<UserCredentialDto?> GetUserCredentialsAsync(string username)
    {
        await using var con = new SqlConnection(_context.connectionstring);
        UserCredentialDto? userCredential = null;
        return await con.QueryFirstOrDefaultAsync<UserCredentialDto>("sp_UserCredential", new { username });
    }
}