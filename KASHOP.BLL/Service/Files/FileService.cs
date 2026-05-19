using Microsoft.AspNetCore.Http;

namespace KASHOP.BLL.Service.Files
{
    public class FileService : IFileService
    {
        public void DeleteAsync(string filename)
        {
            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                filename);
            if(File.Exists(path)) File.Delete(path);
        }

        public async Task<string>? UploadAsync(IFormFile file)
        {
            if(file != null & file.Length  > 0)
            {
                var fileName = Guid.NewGuid().ToString() 
                    +Path.GetExtension(file.FileName);

                var filePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    fileName);

                using(var stream = File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                return fileName;
            }

            return null;
        }
    }
}
