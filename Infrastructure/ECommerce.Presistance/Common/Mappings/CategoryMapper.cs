using AutoMapper;
using ECommerce.Contract.Mappings;
using ECommerce.Data.DTO;
using ECommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistance.Common.Mappings
{
    public class CategoryMapper : /*Profile, */ICategoryMapper
    {
        private readonly IMapper _mapper;

        public CategoryMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void UpdateCategory(Category category, GenericUploadedFileDTO file)
        {
            //CreateMap<CreateCategoryDTO, Category>()
            //    .ForMember(dest => dest.ImageUrl,
            //               o => o.MapFrom(_ => file.FileName))
            //    .ForMember(dest => dest.Id,
            //               o => o.MapFrom(_ => file.MediaId));
            category.ImageUrl = file.FilePath;
            category.Id = file.MediaId;

        }
        public Category MapCategory(CreateCategoryDTO model)
        {
            return new Category
            {
                Name = model.Name,
                Description = model.Description,
                ParentCategoryId = model.ParentCategoryId,

            };
        }
        public CategoryListDTO MapCategoryListDTO(Category category)
        {
            return new CategoryListDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ImageUrl = category.ImageUrl,

            };
        }
    }
}
