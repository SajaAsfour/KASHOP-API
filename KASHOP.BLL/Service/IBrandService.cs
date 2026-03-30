using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public interface IBrandService
    {
        public Task CreateBrandAsync(BrandRequest request);
        public Task<List<BrandResponse>> GetAllBrandsAsync();
    }
}
