using KASHOP.DAL.DTO.Response.Reviews;

namespace KASHOP.DAL.DTO.Response.Products
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserCreated { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public string MainImage { get; set; }
        public List<string> SubImages { get; set; }
        public List<ReviewResponse> Reviews { get; set; }
    }
}
