using Microsoft.Data.SqlClient;
using System.Data;

namespace ToDosAPI.Models.Dapper
{
    public class DapperDBContext
    {
        private readonly IConfiguration _configuration;
        public readonly string connectionstring;
        public DapperDBContext(IConfiguration configuration)
        {
            this._configuration = configuration;
            this.connectionstring = this._configuration.GetConnectionString("connection") ??
             throw new ArgumentNullException("connection string was not found in config");
        }

    }
}
