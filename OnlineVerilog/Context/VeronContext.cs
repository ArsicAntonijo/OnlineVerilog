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
        public DbSet<SolvedExample> SolvedExamples { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SolvedExample>()
                .HasKey(s => new { s.UserId, s.ExampleId });

            builder.Entity<SolvedExample>()
                .HasOne(s => s.SolvedByUser)
                .WithMany(u => u.SolvedExamples)
                .HasForeignKey(s => s.UserId);                
                
            builder.Entity<SolvedExample>()
                .HasOne(s => s.Example)
                .WithMany(e => e.SolvedByUsers)
                .HasForeignKey(s => s.ExampleId);

            builder.Entity<IdentityRole>().HasData(admin, client);
        }
    }
}
