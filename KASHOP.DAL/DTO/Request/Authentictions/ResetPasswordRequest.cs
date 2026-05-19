
namespace KASHOP.DAL.DTO.Request.Authentictions
{
    public class ResetPasswordRequest
    {
        public string Code { get; set; }
        public string NewPassword { get; set; }
        public string Email { get; set; }
    }
}
