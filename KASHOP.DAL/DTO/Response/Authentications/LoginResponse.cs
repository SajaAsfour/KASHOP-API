
namespace KASHOP.DAL.DTO.Response.Authentications
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public string? AccrssToken { get; set; }
    }
}
