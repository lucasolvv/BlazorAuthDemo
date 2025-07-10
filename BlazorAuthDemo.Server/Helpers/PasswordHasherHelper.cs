namespace BlazorAuthDemo.Server.Helpers
{
    public class PasswordHasherHelper
    {
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));
            }
            // Use a secure hashing algorithm, e.g., SHA256
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
        public static bool VerifyPassword(string hashedPassword, string password)
        {
            if (string.IsNullOrEmpty(hashedPassword) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Hashed password and password cannot be null or empty.");
            }
            var hashOfInput = HashPassword(password);
            return hashedPassword == hashOfInput;
        }
    }
}
