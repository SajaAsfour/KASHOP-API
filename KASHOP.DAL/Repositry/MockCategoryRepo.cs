using KASHOP.DAL.Models;
namespace KASHOP.DAL.Repositry
{
    internal class MockCategoryRepo : ICategoryRepository
    {
        public Category Create(Category category)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
