using Microsoft.AspNetCore.Identity.Data;

namespace KvizHub.Interfaces
{
    public interface IUserService
    {
        string SignIn(LoginRequest loginRequest);

        List<string> GetAllUsers();
        string SignUp(RegisterRequest registerRequest);
        string GetProfileImagePath(string username);
    }
}
