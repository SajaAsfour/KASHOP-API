using KASHOP.DAL.DTO.Request.Categories;
using KASHOP.DAL.DTO.Response.Categories;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository.Categories;
using Mapster;
using System.Linq.Expressions;

namespace KASHOP.BLL.Service.Categories
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
            var category = await _categoryRepository.GetOneAsync(c => c.Id == id);
            if(category == null) return false;
            return await _categoryRepository.DeleteAsync(category);
        }

        public async Task<List<CategoryResponse>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync(
                c=>c.Status == EntitiyStatus.Active,
                new string[] 
            { 
                nameof(Category.Translations) ,
                nameof(Category.CreatedBy)
            });

            return categories.Adapt<List<CategoryResponse>>();
        }

        public async Task<CategoryResponse?> GetCategoryAsync(Expression<Func<Category,bool>> filter)
        {
            var category = await _categoryRepository.GetOneAsync(filter ,
                new string[] {
                    nameof(Category.Translations),
                    nameof(Category.CreatedBy)
                });
            return category.Adapt<CategoryResponse>();
        }

        public async Task<bool> ToggleStatusAsync(int id)
        {
            var category = await _categoryRepository.GetOneAsync(c=>c.Id  == id);
            if(category is null) return false;
            category.Status = category.Status == EntitiyStatus.Active ?
                EntitiyStatus.Inactive : EntitiyStatus.Active;
            return await _categoryRepository.UpdateAsync(category);
        }

        public async Task<bool> UpdateCategoryAsync(int id, CategoryUpdateRequest request)
        {
            var category = await _categoryRepository.GetOneAsync(c => c.Id == id,
                new string[]
                {
                    nameof(Category.Translations)
                });

            if (category == null) return false;
            if (request.Translations == null || !request.Translations.Any()) return false;

            foreach (var translationRequest in request.Translations)
            {
                var existingTranslation = category.Translations
                    .FirstOrDefault(t => t.Language == translationRequest.Language);

                if (existingTranslation != null)
                {
                    if (translationRequest.Name != null)
                    {
                        existingTranslation.Name = translationRequest.Name;
                    }
                }
                else
                {
                    if (translationRequest.Name != null)
                    {
                        category.Translations.Add(new CategoryTranslation
                        {
                            Language = translationRequest.Language,
                            Name = translationRequest.Name,
                            CategoryId = category.Id
                        });
                    }
                }
            }

            return await _categoryRepository.UpdateAsync(category);
        }

    }
}
