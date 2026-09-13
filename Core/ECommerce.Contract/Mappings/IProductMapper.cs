using ECommerce.Data.DTO;
using ECommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Contract.Mappings
{
    public interface IProductMapper
    {
        Product MapProduct(CreateProductDTO model, Guid vendorId);
        ProductListDTO MapProductListDTO(Product product);
        void UpdateProductFiles(Product product, List<GenericUploadedFileDTO> files);
    }
}
