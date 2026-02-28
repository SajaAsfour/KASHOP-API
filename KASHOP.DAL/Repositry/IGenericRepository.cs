using KASHOP.DAL.Models;

namespace KASHOP.DAL.Repositry
{
    public interface IGenericRepository <T> where T : class
    {
        Task<List<T>> GetAllAsync(string[]? includes = null);
        Task<T> CreateAsync(T entity);
    }
}
