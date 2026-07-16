using ECommerce.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Presistance.EntitiesConfigurations
{
    public class UserConfiguration
        : IEntityTypeConfiguration<User>
    {
        public void Configure(
            EntityTypeBuilder<User> builder)
        {

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email)
                .IsRequired();
        }
    }
}
