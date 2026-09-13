using AutoMapper;
using ECommerce.Contract.Mappings;
using ECommerce.Data.DTO;
using ECommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistance.Common.Mappings
{
    public class ProductMapper : IProductMapper
    {
        private readonly IMapper _mapper;

        public ProductMapper(IMapper mapper)
        {
            _mapper = mapper;
        }
        public void UpdateProductFiles(Product product, List<GenericUploadedFileDTO> files)
        {
            int order = 0;
            foreach (var file in files)
            {
                product.ProductUploadedFiles.Add(new ProductUploadedFile
                {
                    Id = file.MediaId,
                    Name = file.FilePath,
                    Extension = Path.GetExtension(file.FilePath) ?? string.Empty,
                    ProductId = product.Id,
                    Order = order++
                });
            }
        }
        public Product MapProduct(CreateProductDTO model, Guid vendorId)
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Quantity = model.Quantity,
                CategoryId = model.CategoryId,
                BrandId = model.BrandId,
                VendorId = vendorId,
                ProductUploadedFiles = new List<ProductUploadedFile>()
            };
        }
        public ProductListDTO MapProductListDTO(Product product)
        {
            return new ProductListDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                Category = product.Category.Name,
                Brand = product.Brand?.Name,
                Vendor = product.Vendor.Email,
                ImageUrls = product.ProductUploadedFiles?
                    .OrderBy(x => x.Order)
                    .Select(x => x.Name)
                    .ToList() ?? new List<string>()
            };
        }
    }
}
