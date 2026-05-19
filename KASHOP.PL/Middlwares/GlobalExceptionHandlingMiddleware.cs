using KASHOP.DAL.DTO.Response.ExceptionHandling;

namespace KASHOP.PL.Middlwares
{
    public class GlobalExceptionHandlingMiddleware
    {
        public readonly RequestDelegate _next;
        
        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                ErrorDetailsResponse errorDetails = new ErrorDetailsResponse()
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Server Error ... ",
                    ErrorDetails = ex.InnerException.Message
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(errorDetails);
            }
        }
    }
}
