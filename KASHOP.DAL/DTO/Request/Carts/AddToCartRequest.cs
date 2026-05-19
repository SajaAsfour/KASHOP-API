
namespace KASHOP.DAL.DTO.Request.Carts
{
    public class AddToCartRequest
    {
        public int ProductId { get; set; }
        public int Count { get; set; } = 1;
    }
}
