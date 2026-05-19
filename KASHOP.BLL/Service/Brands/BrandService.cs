using KASHOP.BLL.Service.Files;
using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositry;
using Mapster;
using MapsterMapper;
using System.Linq.Expressions;

namespace KASHOP.BLL.Service.Brands
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository brandRepository
            ,IFileService fileService
            ,IMapper mapper) 
        {
            _brandRepository = brandRepository;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task CreateBrandAsync(BrandRequest request)
        {
            var brand = _mapper.Map<Brand>(request);
            if(brand.Logo != null)
            {
                var imagePath = await _fileService.UploadAsync(request.Logo);
                brand.Logo = imagePath;
            }

            await _brandRepository.CreateAsync(brand);
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            var brand = await _brandRepository.GetOneAsync(b => b.Id == id);
            if (brand == null) return false;
            _fileService.DeleteAsync(brand.Logo);
            return await _brandRepository.DeleteAsync(brand);
        }

        public async Task<List<BrandResponse>> GetAllBrandsAsync()
        {
            var brands = await _brandRepository.GetAllAsync(
                b=>b.Status == EntitiyStatus.Active,
                new string[]
                {
                    nameof(Brand.BrandTranslations),
                    nameof(Brand.CreatedBy)
                });
            return _mapper.Map<List<BrandResponse>>(brands);
        }

        public async Task<BrandResponse?> GetBrandAsync(Expression<Func<Brand, bool>> filter)
        {
            var brand = await _brandRepository.GetOneAsync(filter, new string[]
            {
                nameof(Brand.BrandTranslations),
                nameof(Brand.CreatedBy)
            });
            if (brand == null) return null;
            return _mapper.Map<BrandResponse>(brand);
        }

        public async Task<bool> ToggleStatusAsync(int id)
        {
            var brand = await _brandRepository.GetOneAsync(b=>b.Id == id);
            if (brand is null) return false;
            brand.Status = brand.Status == EntitiyStatus.Active ?
                EntitiyStatus.Inactive : EntitiyStatus.Active;
            return await _brandRepository.UpdateAsync(brand);
        }

        public async Task<bool> UpdateBrandAsync(int id, BrandUpdateRequest request)
        {
            var brand = await _brandRepository.GetOneAsync(b => b.Id == id,
                new string[]
                {
                    nameof(Brand.BrandTranslations)
                });

            if (brand == null) return false;
            if (request.BrandTranslations == null || !request.BrandTranslations.Any())
                return false;

            foreach (var translationRequest in request.BrandTranslations)
            {
                var existing = brand.BrandTranslations
                    .FirstOrDefault(t => t.Language == translationRequest.Language);

                if (existing != null)
                {
                    if (translationRequest.Name != null)
                        existing.Name = translationRequest.Name;
                }
                else
                {
                    if (translationRequest.Name != null)
                    {
                        brand.BrandTranslations.Add(new BrandTranslation
                        {
                            Language = translationRequest.Language,
                            Name = translationRequest.Name,
                            BrandId = brand.Id
                        });
                    }
                }
            }

            var oldLogo = brand.Logo;

            if (request.Logo != null)
            {
                if (!string.IsNullOrEmpty(oldLogo))
                    _fileService.DeleteAsync(oldLogo);

                brand.Logo = await _fileService.UploadAsync(request.Logo);
            }
            else
            {
                brand.Logo = oldLogo;
            }

            return await _brandRepository.UpdateAsync(brand);
        }
    }
}
 