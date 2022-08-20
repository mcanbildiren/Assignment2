using Microsoft.EntityFrameworkCore;

namespace Assignment2.Models
{
    public class Assignment2DbContext : DbContext
    {
        public Assignment2DbContext(DbContextOptions<Assignment2DbContext> options) : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
