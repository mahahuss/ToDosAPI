using Dapper;
using Microsoft.Data.SqlClient;
using ToDosAPI.Models.Entities;

namespace ToDosAPI.Data;

public class PostRepository
{
    private readonly DapperDbContext _context;

    public PostRepository(DapperDbContext context)
    {
        _context = context;
    }

    public async Task<int> addPostsAsync(List<Post> posts)
    {
        int count = 0;
        foreach (var post in posts)
        {
            await using var con = new SqlConnection(_context.ConnectionString);
           var result = await con.QueryFirstOrDefaultAsync<Post>("sp_PostsCreate", new
            {
                post.Id,
                post.Title,
                post.Body
            });
            if (result != null)
            {
                count++;
            }
        }
         return count;
    }


}