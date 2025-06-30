using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole { Id = "Admin", Name = "Admin", NormalizedName = "ADMIN" /* ,ConcurrencyStamp = Guid.NewGuid().ToString() */},
                new IdentityRole { Id = "User", Name = "User", NormalizedName = "USER" /* ,ConcurrencyStamp = Guid.NewGuid().ToString() */}
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }

    }
}