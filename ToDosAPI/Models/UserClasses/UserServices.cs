using Dapper;
using ToDosAPI.Models;
using ToDosAPI.Models.Dapper;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Data.SqlClient;

namespace ToDosAPI.Models.UserClasses
{
    public class UserServices : IUserServices
    {
        private readonly DapperDBContext context;
        public UserServices(DapperDBContext context)
        {
            this.context = context;
        }
        public async Task<String> AddNewUser(User user)
        {
            var response = "";

            string query = " insert into Users (Username, Password,Salt,UserType,FullName) Values (@Username , @Password,@Salt,@UserType,@FullName) ";
            var parameters = new DynamicParameters();
            parameters.Add("Username", user.Username, DbType.String);
            parameters.Add("Password", user.Password, DbType.String);
            parameters.Add("Salt", user.Salt, DbType.String);
            parameters.Add("UserType", user.UserType, DbType.Int64);
            parameters.Add("FullName", user.FullName, DbType.String);
            using (var connectin = this.context.CreateConnection())
            {
              await connectin.ExecuteAsync(query, parameters);
                response = "true";

            }
            return response;
        }

        public async Task<String> login(string Username, string Password)
        {
            var response = "";

            string query = " select * from [Users] where [Username] = @Username and Password = @Password ";
            var parameters = new DynamicParameters();
            parameters.Add("Password", Password, DbType.String);
            parameters.Add("Username", Username, DbType.String);
            using (var connectin = this.context.CreateConnection())
            {
                var user = await connectin.QueryFirstOrDefaultAsync<User>(query, parameters);
                if (user != null)
                    response = "true";
            }
            return response;
        }
    }
}
