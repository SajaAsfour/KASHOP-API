using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;

namespace KASHOP.BLL.Service
{
    public interface ICategoryService
    {
        Task <List<CategoryResponse>> GetAllCategories();
        Task <CategoryResponse> CreateCategory(CategoryRequest request);
    }
}
