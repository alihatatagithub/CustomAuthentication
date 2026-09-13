using ECommerce.Data.DTO.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data.DTO
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? BrandId { get; set; }
        public List<IFormFile>? Files { get; set; }
    }
    public class ProductListFilterDTO : PagingDTO
    {
        public string? Name { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? BrandId { get; set; }
    }
    public class ProductListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Vendor { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
