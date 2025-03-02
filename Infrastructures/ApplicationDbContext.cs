using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ToDoWeb.Domains.Entities;
using ToDoWeb.Infrastructures.DatabaseMapping;

namespace ToDoWeb.Infrastructures
    
{
    
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<School> School { get; set; }
        public DbSet<CourseStudent> CourseStudent { get; set; }
        public DbSet<Course> Courses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=THUDONG\\SQLEXPRESS ; Database=ToDoApp;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .Property(x => x.Age)
                .HasComputedColumnSql("DATEDIFF(YEAR, DATEOFBIRTH, GETDATE())");

            modelBuilder.Entity<Student>()
                .HasMany(student => student.CourseStudents)
                .WithOne(courseStudent => courseStudent.Student)
                .HasForeignKey(courseStudent => courseStudent.StudentId);

            modelBuilder.Entity<Course>()
                .HasMany(course => course.CourseStudents)
                .WithOne(courseStudent => courseStudent.Course)
                .HasForeignKey(courseStudent => courseStudent.CourseId);
            

            modelBuilder.Entity<CourseStudent>()
                .HasKey(courseStudent => new { courseStudent.CourseId, courseStudent.StudentId });

            modelBuilder.ApplyConfiguration(new CourseMapping());
                base.OnModelCreating(modelBuilder);
        }
        public int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
