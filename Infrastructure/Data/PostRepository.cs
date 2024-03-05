using Core.Entities;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Data;

public class PostRepository
{
    private readonly DapperDbContext _context;

    public PostRepository(DapperDbContext context)
    {
        _context = context;
    }

    public async Task<int> AddPostsAsync(List<Post> posts)
    {
        await using var con = new SqlConnection(_context.ConnectionString);
        var count = 0;
        foreach (var post in posts)
        {
            count += await con.ExecuteAsync("sp_PostsCreate", new
            {
                post.Id,
                post.Title,
                post.Body
            });
        }

        return count;
    }
}