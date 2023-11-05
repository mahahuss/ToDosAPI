using Dapper;
using ToDosAPI.Models;
using ToDosAPI.Models.Dapper;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace ToDosAPI.Models.UserClasses
{
    public class UserServices : IUserServices
    {
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        private readonly DapperDBContext _context;
        private readonly IConfiguration _configuration;
        public UserServices(DapperDBContext context, IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
        }
        public async Task<bool> AddNewUser(User user)
        {
            user.Salt = GenerateSalt();
            user.Password = PasswordHashing(user.Password!, user.Salt);

            string query = " insert into Users (Username, Password,Salt,UserType,FullName) ";
            query += " Values (@Username , @Password, @Salt, @UserType, @FullName) ";

            var con = new SqlConnection(_context.connectionstring);
            return await con.ExecuteAsync(query, new { user.Username, user.Password, user.Salt, user.UserType, user.FullName }) > 0;

        }

        private string PasswordHashing (string password, string salt)
        {
            var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            Encoding.UTF8.GetBytes(salt),
            iterations,
            hashAlgorithm,
            keySize);
          
            return Convert.ToHexString(hash);
        }
        private string CheckPassword(string userPassword, string hashedPassword, string userSalt)
        {
            var checking = PasswordHashing(userPassword, userSalt);
            if (hashedPassword == checking){
                return "true";
            }
            return "";
        }

        private string GenerateSalt() {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(keySize));
        }




        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(Sectoken);

            
        }

       public async Task<string> Login(string username, string password)
        {
            string output;
            string query = " select * from Users where Username = @username ";
            var con = new SqlConnection(_context.connectionstring);
            var userInfo = await con.QueryFirstOrDefaultAsync<User>(query, new { username, password }); //check
            output = userInfo != null ? CheckPassword(password, userInfo.Password!, userInfo.Salt!) : "";
            if (output != "")
            {
                var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, userInfo!.Username!, userInfo.FullName, userInfo.UserType.ToString() ),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

                return GenerateToken(authClaims);

            }
            else
                return output!;
        }
    }
}
