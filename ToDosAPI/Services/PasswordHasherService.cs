using System.Security.Cryptography;
using System.Text;

namespace ToDosAPI.Services;

public class PasswordHasherService
{
    private const int KeySize = 64;
    private const int Iterations = 350000;
    private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

    public string HashPassword(string password, string salt)
    {
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            Encoding.UTF8.GetBytes(salt),
            Iterations,
            _hashAlgorithm,
            KeySize);

        return Convert.ToHexString(hash);
    }

    public bool CheckPassword(string userPassword, string hashedPassword, string userSalt)
    {
        var result = HashPassword(userPassword, userSalt);
        return hashedPassword == result;
    }

    public string GenerateSalt()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(KeySize));
    }
}