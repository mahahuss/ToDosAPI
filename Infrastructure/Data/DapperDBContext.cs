using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class DapperDbContext(IConfiguration configuration)
    {
        public readonly string ConnectionString = configuration.GetConnectionString("TodosDb") ??
                                                  throw new Exception("connection string was not found in config");
    }
}