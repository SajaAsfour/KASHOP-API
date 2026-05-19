using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository.Generics;

namespace KASHOP.DAL.Repository.Carts
{
    public class CartRepository : GenericRepository<Cart> , ICartRepository
    {
        public CartRepository(ApplicationDbContext context) : base(context) 
        { 

        }
    }
}
