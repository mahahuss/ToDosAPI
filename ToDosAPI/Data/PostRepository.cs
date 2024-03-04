namespace ToDosAPI.Data;

public class PostRepository(IConfiguration configuration) : DapperDbContext(configuration)
{
    
}