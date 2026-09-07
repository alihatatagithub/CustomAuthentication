using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public Guid? ParentCategoryId { get; set; }

        public Category? ParentCategory { get; set; }

        public ICollection<Category> Children { get; set; } = new List<Category>();
    }
}
