using KASHOP.DAL.DTO.Request.Checkouts;
using KASHOP.DAL.Models;

namespace KASHOP.DAL.DTO.Response.Orders
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }
        public decimal AmountPaid { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public DateTime OrderDate {  get; set; }
    }
}
