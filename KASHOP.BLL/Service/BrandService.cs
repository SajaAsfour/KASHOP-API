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
    }
}
