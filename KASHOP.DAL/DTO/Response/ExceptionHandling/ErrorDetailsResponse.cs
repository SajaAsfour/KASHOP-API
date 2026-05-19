namespace KASHOP.DAL.DTO.Response.ExceptionHandling
{
    public class ErrorDetailsResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string ErrorDetails { get; set; }
    }
}
