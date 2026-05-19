using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;

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
