using Microsoft.EntityFrameworkCore;
using OnlineVerilog.Models;

namespace OnlineVerilog.Context
{
    public class VeronContext : DbContext
    {
        public VeronContext(DbContextOptions<VeronContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Example> Examples { get; set; }
    }
}
