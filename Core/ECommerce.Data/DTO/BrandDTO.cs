using ECommerce.Data.DTO.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data.DTO
{
    public class BrandListFilterDTO : DTOPaging
    {
        public string Name { get; set; }
    }
    public class BrandListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
