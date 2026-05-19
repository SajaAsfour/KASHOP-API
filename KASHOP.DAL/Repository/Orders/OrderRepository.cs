using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository.Generics;

namespace KASHOP.DAL.Repository.Orders
{
    public class OrderRepository : GenericRepository<Order> , IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context) { }
    }
}
