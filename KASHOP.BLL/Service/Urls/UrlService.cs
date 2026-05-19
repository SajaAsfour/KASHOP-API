using Microsoft.AspNetCore.Http;

namespace KASHOP.BLL.Service.Urls
{
    public class UrlService : IUrlService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UrlService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetImageUrl(string? filename)
        {
            if(string.IsNullOrWhiteSpace(filename)) return string.Empty;

            var request = _httpContextAccessor.HttpContext?.Request;

            if (request == null) return $"/images/{filename}";

            var baseUrl = $"{request.Scheme}://{request.Host}";
            return $"{baseUrl}/images/{filename}";
        }
    }
}
