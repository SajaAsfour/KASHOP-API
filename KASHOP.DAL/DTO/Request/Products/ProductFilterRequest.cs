using KASHOP.DAL.DTO.Request.Paginations;

namespace KASHOP.DAL.DTO.Request.Products
{
    public class ProductFilterRequest : PaginationRequest
    {
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public double? MinRate { get; set; }
    }
}
