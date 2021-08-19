using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.Indentity;
using Persistence.Database.config;
using Persistence_Database.config;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Database
{
    public class ApplicationDbContext :
        IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>,
        ApplicationUserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; } 
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new ClientConfig(builder.Entity<Client>());
            new ProductConfig(builder.Entity<Product>());
            new ApplicationUserConfig(builder.Entity<ApplicationUser>());
            new ApplicationRoleConfig(builder.Entity<ApplicationRole>());
        }
    }
}
