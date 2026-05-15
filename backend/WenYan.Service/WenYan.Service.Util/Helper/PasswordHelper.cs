using System.Security.Cryptography;

namespace WenYan.Service.Util
{
    /// <summary>
    /// 密码哈希与校验
    /// 前端已先做 MD5，后端对传入口令再次做强哈希存储
    /// </summary>
    public static class PasswordHelper
    {
        private const string Algorithm = "PBKDF2";
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;

        public static string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            return HashPassword(password, salt);
        }

        public static string HashPassword(string password, byte[] salt)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(password);
            ArgumentNullException.ThrowIfNull(salt);
            if (salt.Length == 0)
                throw new ArgumentException("salt cannot be empty.", nameof(salt));

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);
            return $"{Algorithm}${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
        }

        public static bool VerifyPassword(string password, string storedPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(storedPassword))
                return false;

            return IsHashedPassword(storedPassword) && VerifyHashedPassword(password, storedPassword);
        }

        private static bool IsHashedPassword(string storedPassword)
        {
            return storedPassword.StartsWith($"{Algorithm}$", StringComparison.Ordinal);
        }

        private static bool VerifyHashedPassword(string password, string storedPassword)
        {
            var parts = storedPassword.Split('$', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 4)
                return false;

            if (!int.TryParse(parts[1], out int iterations))
                return false;

            byte[] salt;
            byte[] storedHash;
            try
            {
                salt = Convert.FromBase64String(parts[2]);
                storedHash = Convert.FromBase64String(parts[3]);
            }
            catch (FormatException)
            {
                return false;
            }

            byte[] computedHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, storedHash.Length);
            return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
        }
    }
}
