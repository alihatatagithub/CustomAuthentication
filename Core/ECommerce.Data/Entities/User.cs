using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual RefreshToken RefreshToken { get; set; }

    }
}
