using ECommerce.Contract;
using ECommerce.Contract.Mappings;
using ECommerce.Contract.MediaService;
using ECommerce.Contract.Services;
using ECommerce.Data;
using ECommerce.Data.DTO;
using ECommerce.Data.Entities;
using ECommerce.Ground;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductMapper _mapper;
        private readonly IGenericMapper _genericMapper;
        private readonly IFileStorageService _mediaService;

        public ProductService(IUnitOfWork unitOfWork, IProductMapper mapper, IFileStorageService mediaService, IGenericMapper genericMapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediaService = mediaService;
            _genericMapper = genericMapper;
        }

        public async Task<Response<SuccessDTO>> CreateProduct(CreateProductDTO model, Guid vendorId)
        {
            var product = _mapper.MapProduct(model, vendorId);

            if (model.Files != null && model.Files.Any())
            {
                var files = await _mediaService.UploadAsync(model.Files, Constants.Media.ProductFolder);
                _mapper.UpdateProductFiles(product, files);
            }
            await _unitOfWork.ProductRepository.Create(product);
            await _unitOfWork.SaveChangesAsync();
            return new Response<SuccessDTO>
            {
                IsValid = true,
                Model = new SuccessDTO()
            };

        }

        public async Task<ResponseList<ProductListDTO>> ProductList(ProductListFilterDTO model)
        {
            var productsPagedResult = await _unitOfWork.ProductRepository.GetProducts(model);

            var result = productsPagedResult.List.Select(x => _mapper.MapProductListDTO(x)).ToList();

            return new ResponseList<ProductListDTO>
            {
                IsValid = true,
                Model = result,
                Count = productsPagedResult.TotalCount
            };

        }
    }
}
