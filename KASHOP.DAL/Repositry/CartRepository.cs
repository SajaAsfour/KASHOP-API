using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.DAL.Repositry
{
    public class CartRepository : GenericRepository<Cart> , ICartRepository
    {
        public CartRepository(ApplicationDbContext context) : base(context) 
        { 

        }
    }
}
