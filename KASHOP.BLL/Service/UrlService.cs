using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
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
