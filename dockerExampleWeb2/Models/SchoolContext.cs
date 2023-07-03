using Microsoft.EntityFrameworkCore;
using System.Data;

namespace dockerExampleWeb2.Models
{
    public class SchoolContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public SchoolContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(new Student
            {
                Id = 1,
                FirstName = "Andriy"
            });
        }
    }
}
