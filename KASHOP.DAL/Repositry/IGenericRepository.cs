using KASHOP.DAL.Models;

namespace KASHOP.DAL.Repositry
{
    public interface IGenericRepository <T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
    }
}
