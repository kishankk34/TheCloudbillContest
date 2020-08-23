using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContestApplication.Areas.Identity.Data;
using ContestApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContestApplication.Data
{
    public class ContestApplicationContext : IdentityDbContext<ContestApplicationUser>
    {
        public ContestApplicationContext(DbContextOptions<ContestApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<contest1Table> contest1Table { get; set; }
        public DbSet<contest2Table> contest2Table { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        //    builder.Entity<contest2Table>()
        //.HasNoKey();
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
