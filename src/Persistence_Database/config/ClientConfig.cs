using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence_Database.config
{
    public class ClientConfig
    {
        public ClientConfig(EntityTypeBuilder<Client> entityBuilder)
        {
            entityBuilder.Property(x => x.Name).IsRequired().HasMaxLength(50);
        }

    }
}
