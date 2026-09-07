using ECommerce.Data;
using ECommerce.Data.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Contract.Services
{
    public interface IBrandService
    {
        Task<ResponseList<BrandListDTO>> BrandList(BrandListFilterDTO model);
    }
}
