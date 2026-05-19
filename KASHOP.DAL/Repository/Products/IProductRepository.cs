using KASHOP.DAL.Models;
using KASHOP.DAL.Repository.Generics;

namespace KASHOP.DAL.Repository.Products
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>?> DecreaseQuantityAsync(List<OrderItem> orderItems);
    }
}
