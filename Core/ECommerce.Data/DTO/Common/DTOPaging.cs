using ECommerce.Ground;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ECommerce.Data.DTO.Common
{
    public class DTOPaging
    {
        [DefaultValue(Constants.DefaultPage)]
        public int? Page { get; set; } = Constants.DefaultPage;
        [DefaultValue(Constants.DefaultPageSize)]
        public int? PageSize { get; set; } = Constants.DefaultPageSize;
    }
}
