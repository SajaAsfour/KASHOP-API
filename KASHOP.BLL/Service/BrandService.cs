using KASHOP.DAL.DTO.Request;
using KASHOP.DAL.DTO.Response;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositry;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Service
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

        public async Task<List<BrandResponse>> GetAllBrandsAsync()
        {
            var brands = await _brandRepository.GetAllAsync(
                new string[]
                {
                    nameof(Brand.BrandTranslations),
                    nameof(Brand.CreatedBy)
                });
            return _mapper.Map<List<BrandResponse>>(brands);
        }
    }
}
