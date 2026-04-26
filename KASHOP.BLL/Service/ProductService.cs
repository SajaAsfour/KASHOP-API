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
            product.SubImages = new List<ProductImage>();

            if(request.MainImage != null)
            {
                var imagePath = await _fileService.UploadAsync(request.MainImage);
                product.MainImage = imagePath;
            }

            if(request.SubImages != null)
            {
                foreach(var image in request.SubImages)
                {
                    var imagePath = await _fileService.UploadAsync(image);
                    product.SubImages.Add(new ProductImage { ImagePath = imagePath});

                }
            }

            await _productRepository.CreateAsync(product);
           
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetOneAsync( p => p.Id == id );
            if (product == null) return false;
            _fileService.DeleteAsync(product.MainImage);
            return await _productRepository.DeleteAsync(product);
        }

        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync(
                p=>p.Status == EntitiyStatus.Active
                ,new string[]
            {
                nameof(Product.Translations) ,
                nameof(Product.CreatedBy),
                nameof(Product.SubImages)
            });
            return _mapper.Map<List<ProductResponse>>(products);
        }

        public async Task<ProductResponse?> GetProductAsync(Expression<Func<Product, bool>> filter)
        {
            var product = await _productRepository.GetOneAsync(
                filter,
                new string[]
            {
                nameof(Product.Translations) ,
                nameof(Product.CreatedBy)
            });
            if(product == null) return null;
            
            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<bool> ToggleStatusAsync(int id)
        {
            var product = await _productRepository.GetOneAsync(p=>p.Id == id);
            if(product is null) return false;
            product.Status = product.Status == EntitiyStatus.Active ?
                EntitiyStatus.Inactive : EntitiyStatus.Active;
            return await _productRepository.UpdateAsync(product);
        }

        public async Task<bool> UpdateProductAsync(int id, ProductUpdateRequest request)
        {
            var product = await _productRepository.GetOneAsync(p => p.Id == id,
                new string[]
                {
                    nameof(Product.Translations)
                });
            if (product == null) return false;

            request.Adapt(product);

            if(request.Translations != null)
            {
                foreach(var translationRequest in request.Translations)
                {
                    var existing = product.Translations.FirstOrDefault(t => t.Language == translationRequest.Language);
                    if(existing != null)
                    {
                        if(translationRequest.Name != null)
                            existing.Name = translationRequest.Name;
                        if(translationRequest.Description != null)
                            existing.Description = translationRequest.Description;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            var oldImage = product.MainImage;

            if(request.MainImage != null)
            {
                _fileService.DeleteAsync(oldImage);
                product.MainImage = await _fileService.UploadAsync(request.MainImage);
            }
            else
            {
                product.MainImage = oldImage;
            }

            return await _productRepository.UpdateAsync(product);
        }
    }
}
