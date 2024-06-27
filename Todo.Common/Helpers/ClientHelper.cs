using System.Security.Cryptography;
using System.Text;

namespace Todo.Common.Helpers {
    public static class ClientHelper {
        public static string ComputeHash(string secret, string salt, string pepper, int iteration) {
            if (iteration <= 0) {
                return secret;
            }

            using (var sha256 = SHA256.Create()) {
                var secretSaltPepper = $"{secret}{salt}{pepper}";
                var valueBytes = Encoding.UTF8.GetBytes(secretSaltPepper);
                var hashBytes = sha256.ComputeHash(valueBytes);
                var hash = Convert.ToBase64String(hashBytes);

                return ComputeHash(hash, salt, pepper, iteration - 1);
            }
        }

        public static string GenerateKey() {
            return $"{Guid.NewGuid()}".Replace("-", "").ToLower();
        }

        public static string GenerateSecret() {
            return $"{Guid.NewGuid()}{Guid.NewGuid()}{Guid.NewGuid()}".Replace("-", "").ToLower();
        }

        public static string GenerateSalt() {
            using (var rng = RandomNumberGenerator.Create()) {
                var saltBytes = new byte[16];
                rng.GetBytes(saltBytes);

                return Convert.ToBase64String(saltBytes);
            }
        }
    }
}