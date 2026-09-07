using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data.Entities
{
    public class ProductUploadedFile
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Order { get; set; }
    }
}
