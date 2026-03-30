using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositry;
using Mapster;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository 
            , IFileService fileService
            ,IMapper mapper) 
        {
            _productRepository = productRepository;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task CreateProductAsync(ProductRequest request)
        {
            var product = _mapper.Map<Product>(request);
            if(request.MainImage != null)
            {
                var imagePath = await _fileService.UploadAsync(request.MainImage);
                product.MainImage = imagePath;
            }
            await _productRepository.CreateAsync(product);
           
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetOne( p => p.Id == id );
            if (product == null) return false;
            _fileService.Delete(product.MainImage);
            return await _productRepository.DeleteAsync(product);
        }

        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync(new string[]
            {
                nameof(Category.Translations) ,
                nameof(Category.CreatedBy)
            });
            return _mapper.Map<List<ProductResponse>>(products);
        }

        public async Task<ProductResponse?> GetProductAsync(Expression<Func<Product, bool>> filter)
        {
            var product = await _productRepository.GetOne(
                filter,
                new string[]
            {
                nameof(Category.Translations) ,
                nameof(Category.CreatedBy)
            });
            if(product == null) return null;
            
            return _mapper.Map<ProductResponse>(product);
        }
    }
}
