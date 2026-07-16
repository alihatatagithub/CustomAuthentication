using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual User User { get; set; }
    }
}
