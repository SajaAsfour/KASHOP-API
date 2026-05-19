using System.Text.Json.Serialization;

namespace KASHOP.DAL.DTO.Request.Checkouts
{
    public enum PaymentMethodEnum
    {
        Cash = 1,
        Visa = 2,
    }
    public class CheckoutRequest
    {
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PhoneNumber { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentMethodEnum PaymentMethod { get; set; }
    }
}
