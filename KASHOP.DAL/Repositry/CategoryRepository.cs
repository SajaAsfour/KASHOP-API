using KASHOP.DAL.Data;
using KASHOP.DAL.Models;

namespace KASHOP.DAL.Repositry
{
    public class CategoryRepository : GenericRepository<Category> , ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) :base(context)
        {
        }
    }
}
