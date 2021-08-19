using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Indentity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Database.config
{
    class ApplicationRoleConfig
    {

        public ApplicationRoleConfig(EntityTypeBuilder<ApplicationRole> entityBuilder)
        {
            entityBuilder.HasMany(e => e.UserRoles)
                        .WithOne(e => e.Role)
                        .HasForeignKey(e => e.RoleId)
                        .IsRequired();

            entityBuilder.HasData(
                new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "Admin"
                },
                new ApplicationRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Seller",
                    NormalizedName = "Seller"
                }

                );
        }

    }
}
