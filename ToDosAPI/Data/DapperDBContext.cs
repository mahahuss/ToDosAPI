namespace ToDosAPI.Data
{
    public class DapperDbContext
    {
        private readonly IConfiguration _configuration;
        public readonly string connectionstring;
        public DapperDbContext(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.connectionstring = this._configuration.GetConnectionString("connection") ??
             throw new ArgumentNullException("connection string was not found in config");
        }

    }
}
