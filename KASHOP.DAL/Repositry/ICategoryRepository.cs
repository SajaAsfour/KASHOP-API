using KASHOP.DAL.Models;

namespace KASHOP.DAL.Repositry
{
    public interface ICategoryRepository
    {
       Task<List<Category>> GetAllAsync();
       Task<Category> CreateAsync(Category category);
    }
}
