using KASHOP.DAL.Models;
using System.Linq.Expressions;

namespace KASHOP.DAL.Repositry
{
    public interface IGenericRepository <T> where T : class
    {
        Task<List<T>> GetAllAsync(string[]? includes = null);
        Task<T> CreateAsync(T entity);
        Task<T> GetOne(Expression<Func<T, bool>> filter, string[]? includes = null);
    }
}
