using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineVerilog.Models;

namespace OnlineVerilog.Context
{
    public class VeronContext : IdentityDbContext<User>
    {
        public VeronContext(DbContextOptions<VeronContext> options)
            : base(options) { }

       // public DbSet<User> Users { get; set; }
        public DbSet<Example> Examples { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var client = new IdentityRole("client");
            client.NormalizedName = "client";

            builder.Entity<IdentityRole>().HasData(admin, client);
        }
    }
}
