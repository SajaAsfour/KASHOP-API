
namespace KASHOP.DAL.DTO.Response.Checkouts
{
    public class CheckoutResponse
    {
        public int OrderId { get; set; }
        public string? StripeUrl { get; set; }
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}
