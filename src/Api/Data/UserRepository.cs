using Dapper;
using Microsoft.Data.SqlClient;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Api.Models;
using Api.Models.Dtos;
using Api.Models.Entities;

namespace Api.Data;

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

    public async Task<List<GetUsers>> GetUsersAsync(int currentUserId)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        return (await con.QueryAsync<GetUsers>("sp_UserGetAll", new { userId = currentUserId })).ToList();
    }

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

    public async Task<List<Role>> GetAllRolesAsync()
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        return (await con.QueryAsync<Role>("sp_RolesGet")).ToList();
    }

    public async Task<bool> EditUserProfileAsync(EditProfileByAdminDto editProfileByAdminDto)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        /*var check = */
        await con.ExecuteAsync("sp_UsersRolesDelete", new { editProfileByAdminDto.Id }); /* > 0;*/
        //if (!check) return false;

        foreach (Role role in editProfileByAdminDto.Roles)
        {
            await con.QueryAsync<Role>("sp_UsersEditRoles", new { editProfileByAdminDto.Id, editProfileByAdminDto.Fullname, Role= role.Id });
        }

        return true;
    }

    public async Task<List<UsersToShareDto>> GetUserstoShareAsync(int userId)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        return (await con.QueryAsync<UsersToShareDto>("sp_UsersGetToShare", new { userId } )).ToList();
    }
}