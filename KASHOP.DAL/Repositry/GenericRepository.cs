using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KASHOP.DAL.Repositry
{
    public class GenericRepository <T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Remove(entity);
            var affected = await _context.SaveChangesAsync();
            return affected > 0;
        }

        public async Task<bool> DeleteRangeAsync(List<T> entites)
        {
            _context.RemoveRange(entites);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T,bool>> filter,string[]? includes = null)
        {
            IQueryable <T> query = _context.Set<T>();

            if(filter!= null)
                query = query.Where(filter);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<T?> GetOneAsync(Expression<Func<T,bool>> filter,string[]? includes =null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Update(entity);
            var affected = await _context.SaveChangesAsync();
            return affected > 0;
        }

        public async Task<bool> UpdateRangeAsync(List<T> entites)
        {
            _context.UpdateRange(entites);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
