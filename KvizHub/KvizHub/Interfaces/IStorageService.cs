using Microsoft.AspNetCore.Http;

namespace KvizHub.Interfaces
{
    public interface IStorageService
    {
        string Upload(IFormFile sourceFile, string destinationDirectory="uploads");
 
        void DeleteFile(string storedFileName, string formDirectory="uploads");
    }
}
