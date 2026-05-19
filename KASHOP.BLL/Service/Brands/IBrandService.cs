using KASHOP.DAL.DTO.Request.Brands;
using KASHOP.DAL.DTO.Response.Brands;
using KASHOP.DAL.Models;

using System.Linq.Expressions;

namespace KASHOP.BLL.Service.Brands
{
    public interface IBrandService
    {
        Task CreateBrandAsync(BrandRequest request);
        Task<List<BrandResponse>> GetAllBrandsAsync();
        Task<BrandResponse?> GetBrandAsync(Expression<Func<Brand, bool>> filter);
        Task<bool> DeleteBrandAsync(int id);
        Task<bool> UpdateBrandAsync(int id , BrandUpdateRequest request);
        Task<bool> ToggleStatusAsync(int id);
    }
}
