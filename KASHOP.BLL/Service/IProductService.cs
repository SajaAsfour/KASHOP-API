using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using KASHOP.DAL.Models;

namespace KASHOP.BLL.Service
{
    public interface IProductService
    {
        Task CreateProductAsync(ProductRequest request);
        Task<PaginationResponse<ProductResponse>> GetAllProductsAsync(PaginationRequest request);
        Task<ProductResponse?> GetProductAsync(Expression<Func<Product, bool>> filter);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> UpdateProductAsync(int id, ProductUpdateRequest request);
        Task<bool> ToggleStatusAsync(int id);
        Task<bool> DeleteSubImageAsync(int productId, int imageId);
    }
}
