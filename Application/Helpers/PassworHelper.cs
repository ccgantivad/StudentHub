using System.Security.Cryptography;
using System.Text;

namespace Application.Helpers
{
    public static class PasswordHelper
    {
        public static (string Hash, string Key) HashPassword(string password)
        {
            using var hmac = new HMACSHA512();
            var key = Convert.ToBase64String(hmac.Key);
            var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return (hash, key);
        }

        public static bool VerifyPasswordHash(string password, string storedHash, string storedKey)
        {
            var keyBytes = Convert.FromBase64String(storedKey);
            using var hmac = new HMACSHA512(keyBytes);
            var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            return computedHash == storedHash;
        }
    }
}
