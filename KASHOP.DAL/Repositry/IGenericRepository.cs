using KASHOP.DAL.Models;
using System.Linq.Expressions;

namespace KASHOP.DAL.Repositry
{
    public interface IGenericRepository <T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter, string[]? includes = null);
        Task<T> CreateAsync(T entity);
        Task<T?> GetOneAsync(Expression<Func<T, bool>> filter, string[]? includes = null);
        Task<bool> DeleteAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteRangeAsync(List<T>  entites);
    }
}
