using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository.Generics;

namespace KASHOP.DAL.Repository.Brands
{
    public class BrandRepository : GenericRepository<Brand> , IBrandRepository
    {
        public BrandRepository(ApplicationDbContext context) : base(context) 
        {

        }
    }
}
