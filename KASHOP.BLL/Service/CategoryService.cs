using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositry;
using Mapster;

namespace KASHOP.BLL.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) 
        { 
            _categoryRepository = categoryRepository;
        }
        public CategoryResponse CreateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            _categoryRepository.Create(category);

            return category.Adapt<CategoryResponse>();
        }

        public List<CategoryResponse> GetAllCategories()
        {
            var categories = _categoryRepository.GetAll();

            return categories.Adapt<List<CategoryResponse>>();
        }
    }
}
