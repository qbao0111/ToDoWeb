using Microsoft.EntityFrameworkCore;
using ToDoWeb.Domains.Entities;

namespace ToDoWeb.Infrastructures
{
    public interface IApplicationDbContext
    {
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<School> School { get; set; }

        public DbSet<CourseStudent> CourseStudent { get; set; }
        public DbSet<Course> Courses { get; set; }
        public int SaveChanges();
    }
}
