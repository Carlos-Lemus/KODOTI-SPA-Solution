using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence_Database.config
{
    public class ProductConfig
    {
        public ProductConfig(EntityTypeBuilder<Product> entityBuilder)
        {
            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            entityBuilder.Property(x => x.Description).IsRequired().HasMaxLength(250);
        }

    }
}
