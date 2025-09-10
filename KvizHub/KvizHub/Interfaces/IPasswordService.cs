namespace KvizHub.Interfaces
{
    public interface IPasswordService
    {
        string Encrypt(string plainTextPassword);

        bool Validate(string plainTextPassword, string encryptedPassword);
    }
}
