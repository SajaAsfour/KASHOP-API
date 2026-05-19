using KASHOP.DAL.DTO.Request.Authentictions;
using KASHOP.DAL.DTO.Response.Authentications;

namespace KASHOP.BLL.Service.Authentication
{
    public interface IAuthenticationService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<bool> ConfirmEmailAsync(string token, string userId);
        Task<ForgetPasswordResponse> RequestPasswordForgetAsync (ForgetPasswordRequest request);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task<LoginResponse> RefreshTokenAsync();
    }
}
