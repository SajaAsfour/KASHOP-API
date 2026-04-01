using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using System.Linq.Expressions;

namespace KASHOP.BLL.Service
{
    public interface ICategoryService
    {
        Task <List<CategoryResponse>> GetAllCategoriesAsync();
        Task <CategoryResponse> CreateCategoryAsync(CategoryRequest request);
        Task<CategoryResponse?> GetCategoryAsync(Expression<Func<Category, bool>> filter);
        Task<bool> DeleteCategoryAsync(int id);
        Task<bool> UpdateCategoryAsync(int id, CategoryUpdateRequest request);
        Task<bool> ToggleStatusAsync(int id);
    }
}
