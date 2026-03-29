using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using KASHOP.DAL.Models;

namespace KASHOP.BLL.Service
{
    public interface IProductService
    {
        public Task CreateProductAsync(ProductRequest request);
        public Task<List<ProductResponse>> GetAllProductsAsync();
        public Task<ProductResponse?> GetProductAsync(Expression<Func<Product, bool>> filter);
    }
}
