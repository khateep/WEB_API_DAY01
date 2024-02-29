using Microsoft.EntityFrameworkCore;
using WEB_API_DAY01.Models;

namespace WEB_API_DAY01.Entity
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions options) : base(options) 
        { 
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=api2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False");
            }
        }
        public DbSet<Student> students { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
