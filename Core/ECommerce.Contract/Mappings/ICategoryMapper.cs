using ECommerce.Data.DTO;
using ECommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Contract.Mappings
{
    public interface ICategoryMapper
    {
        Category MapCategory(CreateCategoryDTO model);
        CategoryListDTO MapCategoryListDTO(Category category);
        void UpdateCategory(Category category, GenericUploadedFileDTO file);
    }
}
