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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICategoryMapper _mapper;
        private readonly IGenericMapper _genericMapper;
        private readonly IFileStorageService _mediaService;

        public CategoryService(IUnitOfWork unitOfWork, ICategoryMapper mapper, IFileStorageService mediaService, IGenericMapper genericMapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mediaService = mediaService;
            _genericMapper = genericMapper;
        }

        public async Task<Response<SuccessDTO>> CreateCategory(CreateCategoryDTO model)
        {
            var category = _genericMapper.Map<Category,CreateCategoryDTO>(model);

            if (model.File != null)
            {
                var file = await _mediaService.UploadAsync(new List<IFormFile> { model.File }, Constants.Media.CategoryFolder);
                _mapper.UpdateCategory(category, file.First());
            }
            await _unitOfWork.CategoryRepository.Create(category);
            await _unitOfWork.SaveChangesAsync();
            return new Response<SuccessDTO>
            {
                IsValid = true,
                Model = new SuccessDTO()
            };

        }

        public async Task<ResponseList<CategoryListDTO>> CategoryList(CategoryListFilterDTO model)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            var rootCategories = categories.Where(x => x.ParentCategoryId == null).ToList();

            var result =  rootCategories.Select(x => BuildTree(x, categories)).ToList();

            return new ResponseList<CategoryListDTO>
            {
                IsValid = true,
                Model = result,
            };

        }
        private CategoryListDTO BuildTree(Category rootCategory, List<Category> allCategories)
        {
            var dto = _genericMapper.Map<CategoryListDTO,Category>(rootCategory);

            var children = allCategories
                .Where(x => x.ParentCategoryId == rootCategory.Id)
                .ToList();

            foreach (var child in children)
            {
                var childDto = BuildTree(child, allCategories);

                dto.Children.Add(childDto);
            }
            //dto.Children = children
            //    .Select(child => BuildTree(child, allCategories))
            //    .ToList();

            return dto;
        }
    }
}
