using ECommerce.Data.DTO.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data.DTO
{
    public class CreateCategoryDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public IFormFile? File { get; set; }
    }
    public class CategoryListFilterDTO 
    {
        public string Name { get; set; }
    }
    public class CategoryListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public List<CategoryListDTO> Children { get; set; }
    }
}
