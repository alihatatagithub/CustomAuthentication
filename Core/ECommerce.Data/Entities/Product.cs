using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        public Guid? BrandId { get; set; }

        public Brand? Brand { get; set; }
        public Guid VendorId { get; set; }

        public User Vendor { get; set; }
        public ICollection<ProductUploadedFile> ProductUploadedFiles { get; set; }
    }
}
