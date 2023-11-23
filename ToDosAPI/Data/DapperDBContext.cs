namespace ToDosAPI.Data
{
    public class DapperDbContext
    {
        public readonly string ConnectionString;

        public DapperDbContext(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("TodosDb") ??
                               throw new ArgumentNullException("connection string was not found in config");
        }
    }
}