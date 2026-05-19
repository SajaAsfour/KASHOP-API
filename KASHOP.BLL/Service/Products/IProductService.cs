using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using System.Linq.Expressions;
using KASHOP.DAL.Models;

namespace KASHOP.BLL.Service.Products
{
    public interface IProductService
    {
        Task CreateProductAsync(ProductRequest request);
        Task<PaginationResponse<ProductResponse>> GetAllProductsAsync(ProductFilterRequest request);
        Task<ProductResponse?> GetProductAsync(Expression<Func<Product, bool>> filter);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> UpdateProductAsync(int id, ProductUpdateRequest request);
        Task<bool> ToggleStatusAsync(int id);
        Task<bool> DeleteSubImageAsync(int productId, int imageId);
    }
}
