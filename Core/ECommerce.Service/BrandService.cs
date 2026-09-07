using ECommerce.Contract;
using ECommerce.Contract.Mappings;
using ECommerce.Contract.MediaService;
using ECommerce.Contract.Services;
using ECommerce.Data;
using ECommerce.Data.DTO;
using ECommerce.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Service
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryMapper _mapper;
        private readonly IGenericMapper _genericMapper;
        private readonly IFileStorageService _mediaService;

        public BrandService(IUnitOfWork unitOfWork, ICategoryMapper mapper, IFileStorageService mediaService, IGenericMapper genericMapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediaService = mediaService;
            _genericMapper = genericMapper;
        }

        public async Task<ResponseList<BrandListDTO>> BrandList(BrandListFilterDTO model)
        {
            var brandsPagesResult = await _unitOfWork.BrandRepository.GetBrands(model);
            var dtos = _genericMapper.Map<List<BrandListDTO>, List<Brand>>(brandsPagesResult.List);
            return new ResponseList<BrandListDTO>
            {
                IsValid = true,
                Model = dtos,
                Count = brandsPagesResult.TotalCount
            };

        }
    }
}
