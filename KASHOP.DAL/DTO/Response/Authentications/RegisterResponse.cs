namespace KASHOP.DAL.DTO.Response.Authentications
{
    public class RegisterResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public List<string>? Errors { get; set; }
    }
}
