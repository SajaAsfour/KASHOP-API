using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositry;
using Mapster;
using System.Linq.Expressions;

namespace KASHOP.BLL.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) 
        { 
            _categoryRepository = categoryRepository;
        }
        public async Task <CategoryResponse> CreateCategoryAsync(CategoryRequest request)
        {
            var category = request.Adapt<Category>();
            await _categoryRepository.CreateAsync(category);

            return category.Adapt<CategoryResponse>();
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetOne(c => c.Id == id);
            if(category == null) return false;
            return await _categoryRepository.DeleteAsync(category);
        }

        public async Task<List<CategoryResponse>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync(new string[] 
            { 
                nameof(Category.Translations) ,
                nameof(Category.CreatedBy)
            });

            return categories.Adapt<List<CategoryResponse>>();
        }

        public async Task<CategoryResponse?> GetCategoryAsync(Expression<Func<Category,bool>> filter)
        {
            var category = await _categoryRepository.GetOne(filter ,
                new string[] {
                    nameof(Category.Translations),
                    nameof(Category.CreatedBy)
                });
            return category.Adapt<CategoryResponse>();
        }
    }
}
