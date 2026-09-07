using ECommerce.Data.DTO;
using ECommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Contract.Mappings
{
    public interface ICategoryMapper
    {
        void UpdateCategory(Category category, GenericUploadedFileDTO file);
    }
}
