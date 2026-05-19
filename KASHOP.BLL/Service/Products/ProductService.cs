using KASHOP.BLL.Extensions;
using KASHOP.BLL.Service.Files;
using KASHOP.DAL.DTO.Request.Products;
using KASHOP.DAL.DTO.Response.Paginations;
using KASHOP.DAL.DTO.Response.Products;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repository.Products;
using Mapster;
using MapsterMapper;
using System.Linq.Expressions;

namespace KASHOP.BLL.Service.Products
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
            var product = await _productRepository.GetOneAsync( p => p.Id == id ,
                new string[]
                {
                    nameof(Product.SubImages)
                });
            if (product == null) return false;
            _fileService.DeleteAsync(product.MainImage);

            foreach(var image in product.SubImages)
            {
                _fileService.DeleteAsync(image.ImagePath);
            }

            return await _productRepository.DeleteAsync(product);
        }

        public async Task<bool> DeleteSubImageAsync(int productId, int imageId)
        {
            var product = await _productRepository.GetOneAsync(
                filter: p => p.Id == productId,
                includes: new[]
                {
                    nameof(Product.SubImages)
                });

            if(product == null) return false;

            var image = product.SubImages.FirstOrDefault(x => x.Id == imageId);

            if(image == null) return false;

            _fileService.DeleteAsync(image.ImagePath);

            product.SubImages.Remove(image);

            return await _productRepository.UpdateAsync(product);
        }

        public async Task<PaginationResponse<ProductResponse>> GetAllProductsAsync(ProductFilterRequest request)
        {
            var query = _productRepository.GetQueryable(
                p=>p.Status == EntitiyStatus.Active
                ,new string[]
            {
                nameof(Product.Translations) ,
                nameof(Product.CreatedBy),
                nameof(Product.SubImages),
                nameof(Product.Reviews)
            });

            //search 
            if(request.Search != null)
            {
                query = query.Where(p => p.Translations.Any(t => t.Name.Contains(request.Search)));
            }

            //filter
            if (request.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == request.CategoryId);

            if(request.MinPrice.HasValue)
                query = query.Where(p => p.Price >= request.MinPrice);

            if(request.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= request.MaxPrice);

            if(request.MinRate.HasValue)
                query = query.Where(p => p.Rate  >= request.MinRate);

            var paginated = await query.ToPaginationAsync(request.Page, request.Limit);
            return new PaginationResponse<ProductResponse> 
            {
                Data = _mapper.Map<List<ProductResponse>>(paginated.Data),
                TotalCount = paginated.TotalCount,
                Page = paginated.Page,
                Limit = paginated.Limit
            };
        }

        public async Task<ProductResponse?> GetProductAsync(Expression<Func<Product, bool>> filter)
        {
            var product = await _productRepository.GetOneAsync(
                filter,
                new string[]
            {
                nameof(Product.Translations) ,
                nameof(Product.CreatedBy),
                nameof(Product.SubImages),
                nameof(Product.Reviews)
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
                    nameof(Product.Translations),
                    nameof(Product.SubImages)
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

            if (request.SubImages != null)
            {
                foreach (var oldSubImage in product.SubImages)
                {
                    _fileService.DeleteAsync(oldSubImage.ImagePath);
                }

                product.SubImages.Clear();

                foreach (var image in request.SubImages)
                {
                    var imagePath = await _fileService.UploadAsync(image);

                    product.SubImages.Add(new ProductImage
                    {
                        ImagePath = imagePath,
                    });
                }
            }

            if(request.NewImages != null)
            {
                foreach (var image in request.NewImages)
                {
                    var imagePath = await _fileService.UploadAsync(image);

                    product.SubImages.Add(new ProductImage
                    {
                        ImagePath = imagePath,
                    });
                }
            }

            return await _productRepository.UpdateAsync(product);
        }
    }
}
