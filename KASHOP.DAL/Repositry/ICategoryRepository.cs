using KASHOP.DAL.Models;

namespace KASHOP.DAL.Repositry
{
    public interface ICategoryRepository
    {
        List <Category> GetAll();
        Category Create(Category category);
    }
}
