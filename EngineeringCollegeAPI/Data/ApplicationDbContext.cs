using EngineeringCollegeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EngineeringCollegeAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Email)
                .IsUnique();

            modelBuilder.Entity<Course>().HasData(
                new Course { CourseId = 1, CourseName = "Computer Science" },
                new Course { CourseId = 2, CourseName = "Electrical And Communication Engineering" },
                new Course { CourseId = 3, CourseName = "Mechanical Engineering" },
                new Course { CourseId = 4, CourseName = "Civil Engineering" },
                new Course { CourseId = 5, CourseName = "Electrical and Electronics Engineering" }
                );
        }
    }
}
