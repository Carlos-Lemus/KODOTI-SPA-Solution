using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model.Indentity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Database.config
{
    public class ApplicationUserConfig 
    {

        public ApplicationUserConfig(EntityTypeBuilder<ApplicationUser> entityBuilder)
        {
            entityBuilder.HasMany(e => e.UserRoles)
                        .WithOne(e => e.User)
                        .HasForeignKey(e => e.UserId)
                        .IsRequired();
        }

    }
}
