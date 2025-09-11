using KvizHub.Interfaces;

namespace KvizHub.Services
{
    public class StorageService : IStorageService
    {
        private readonly IWebHostEnvironment _env;
        public StorageService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public void DeleteFile(string storedFileName, string formDirectory = "uploads")
        {
            if(string.IsNullOrWhiteSpace(storedFileName))
                return;

            var fullPath = Path.Combine(_env.WebRootPath, formDirectory, storedFileName);

            if(File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public string Upload(IFormFile sourceFile, string destinationDirectory = "uploads")
        {
            var uploadPath = Path.Combine(_env.WebRootPath, destinationDirectory);

            if(!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(sourceFile.FileName);
            var fullPath = Path.Combine(uploadPath, uniqueFileName);
            using(var stream = new FileStream(fullPath, FileMode.Create))
            {
                sourceFile.CopyTo(stream);
            }
            return uniqueFileName;
        }
    }
}
