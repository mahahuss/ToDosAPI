using Dapper;
using Microsoft.Data.SqlClient;
using ToDosAPI.Models.UserClasses;

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
        var query = " insert into Users (Username, Password,Salt,UserType,FullName) ";
        query += " Values (@Username , @Password, @Salt, @UserType, @FullName) ";

        await using var con = new SqlConnection(_context.connectionstring);
        return await con.QueryFirstOrDefaultAsync<User>(query, new { user.Username, user.Password, user.Salt, user.UserType, user.FullName });
    }

    public async Task<User?> GetUserAsync(string username)
    {
        const string query = " select * from Users where Username = @username";
        await using var con = new SqlConnection(_context.connectionstring);
        return await con.QueryFirstOrDefaultAsync<User>(query, new { username });
    }
}