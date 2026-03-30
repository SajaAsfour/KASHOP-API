using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public interface IBrandService
    {
        public Task CreateBrandAsync(BrandRequest request);
        public Task<List<BrandResponse>> GetAllBrandsAsync();
        public Task<BrandResponse?> GetBrandAsync(Expression<Func<Brand, bool>> filter);
        public Task<bool> DeleteBrandAsync(int id);
    }
}
