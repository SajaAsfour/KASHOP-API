using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;

namespace KASHOP.BLL.Service
{
    public interface ICategoryService
    {
        List<CategoryResponse> GetAllCategories();
        CategoryResponse CreateCategory(CategoryRequest request);
    }
}
