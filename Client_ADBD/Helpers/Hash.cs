using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.Helpers
{
    internal class Hash
    {
        private static readonly int saltLength = 64;

        public static string GenerateSalt()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var saltBytes = new byte[saltLength];
                rng.GetBytes(saltBytes);
                return Convert.ToBase64String(saltBytes);
            }
        }
        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {

                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var saltBytes = Convert.FromBase64String(salt);
                var passwordWithSalt = passwordBytes.Concat(saltBytes).ToArray();
                var hashedBytes = sha256.ComputeHash(passwordWithSalt);

                return Convert.ToBase64String(hashedBytes);
            }
        }
        public static bool VerifyPassword(string enteredPassword, string storedPasswordHash, string storedSalt)
        {
            var enteredPasswordHash = HashPassword(enteredPassword, storedSalt);
            return enteredPasswordHash == storedPasswordHash; 
        }
    }
}
