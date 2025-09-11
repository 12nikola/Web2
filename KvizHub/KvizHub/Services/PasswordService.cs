using KvizHub.Interfaces;

namespace KvizHub.Services
{
    public class PasswordService : IPasswordService
    {
        public string Encrypt(string plainTextPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainTextPassword);
        }

        public bool Validate(string plainTextPassword, string encryptedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainTextPassword, encryptedPassword);
        }
    }
}