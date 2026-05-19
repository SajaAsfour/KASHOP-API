using Microsoft.AspNetCore.Http;

namespace KASHOP.BLL.Service.Files
{
    public interface IFileService
    {
        Task<string>? UploadAsync(IFormFile file);
        void DeleteAsync(string filename);
    }
}
