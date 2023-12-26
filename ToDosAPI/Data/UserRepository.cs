using Dapper;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using ToDosAPI.Models;
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
        await using var con = new SqlConnection(_context.ConnectionString);
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
        await using var con = new SqlConnection(_context.ConnectionString);
        UserWithRolesDto? userWithRoles = null;
        await con.QueryAsync<AppUser, Role, UserWithRolesDto>("sp_UserGetUserWithRoles",
            (user, role) =>
            {
                userWithRoles ??= new UserWithRolesDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Username = user.Username,
                    Status = user.Status
                };

                userWithRoles.Roles.Add(role.UserType);
                return userWithRoles;
            }, new { username });

        return userWithRoles;
    }


    public async Task<UserCredentialDto?> GetUserCredentialsAsync(string username)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        return await con.QueryFirstOrDefaultAsync<UserCredentialDto>("sp_UserCredential", new { username });
    }


    public async Task<bool> EditUserProfileAsync(string name, int id)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        return await con.ExecuteAsync("sp_UserEdit", new { name, id }) > 0;
    }

    public async Task<List<GetUsers>> GetUsersAsync()
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        return (await con.QueryAsync<GetUsers>("sp_UserGetAll")).ToList();
    }


    //public async Task<List<GetUsersWithRoles>> GetUsersAsync()
    //{
    //    await using var con = new SqlConnection(_context.ConnectionString);
    //    GetUsersWithRoles? getUsersWithRoles = null;
    //    List<GetUsersWithRoles> allUsers = new List<GetUsersWithRoles>();
    //    var users = new Dictionary<int, GetUsersWithRoles>();

    //    //return (await con.QueryAsync<GetUsers>("sp_UserGetAll")).ToList();

    //    await con.QueryAsync<GetUsers, Role, GetUsersWithRoles>("sp_UserGetAll",
    //              (user, role) =>
    //              {
    //                  getUsersWithRoles ??= new GetUsersWithRoles
    //                  {
    //                      Id = user.Id,
    //                      FullName = user.FullName,
    //                      Username = user.Username,
    //                      Status = user.Status,
    //                      TotalTasks = user.TotalTasks,
    //                  };

    //                  getUsersWithRoles.roles.Add(role.Id, role.UserType);

    //                  allUsers.Add(getUsersWithRoles);
    //                  getUsersWithRoles.roles.Clear();

    //                  return getUsersWithRoles;
    //              });
    //    return allUsers;
    //}

    public async Task<bool> ChangeUserStatusAsync(int userId, bool status)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        return await con.ExecuteAsync("sp_UserChangeStatus", new { userId, status }) > 0;
    }

    public async Task<List<Role>> GetUserRolesAsync(int userId)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        return (await con.QueryAsync<Role>("sp_UsersRolesGet", new { userId }) ).ToList();
    }

    public async Task<List<Role>> GetAllRolesAsync(int userId)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        return (await con.QueryAsync<Role>("sp_RolesGet", new { userId })).ToList();
    }
}