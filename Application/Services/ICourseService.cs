using System.Net;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Application.Dtos;
using ToDoWeb.Application.Dtos;
using ToDoWeb.Domains.Entities;
using ToDoWeb.Infrastructures;

namespace ToDoWeb.Application.Services
{
    public interface ICourseService
    {
        IEnumerable<CourseViewModel> GetCourses(string? courseId);
        CourseDetailModel GetCourseDetail(int id);

        void AssignCourse(int StudentId, int CourseId);
        int UpgradeGrades(int StudentId, int CourseId, float Assignment, float Final, float Practical);
        GradesDetailModel GetGradesDetail(int id);
        GpaViewModel GetGPA(int id);

        int PostCourse(CourseCreateModel course);

        int PutCourse(CourseUpdateModel course);

        void DeleteCourse(int courseId);
    }

    public class CourseService : ICourseService
    {
        private readonly IApplicationDbContext _context;
        public CourseService(IApplicationDbContext context)
        {
            _context = context;
        }

        public void DeleteCourse(int courseId)
        {
            var data = _context.Courses.Find(courseId);
            if (data == null) return;

            _context.Courses.Remove(data);
            _context.SaveChanges();
        }

        public IEnumerable<CourseViewModel> GetCourses(string? courseId)
        {
            var query = _context.Courses.AsQueryable();



            return query
                .Select(x => new CourseViewModel
                {
                    CourseId = x.Id,
                    CourseName = x.Name,
                    StartDate = x.StartDate
                })
                .ToList();
        }

        public int PostCourse(CourseCreateModel course)
        {
            var data = new Course
            {
                Name = course.CourseName,
                StartDate = course.StartDate,

            };


            _context.Courses.Add(data);
            _context.SaveChanges();

            return data.Id;
        }

        public int PutCourse(CourseUpdateModel course)
        {
            var data = _context.Courses.Find(course.CourseId);
            if (data == null) return -1;

            if (!string.IsNullOrWhiteSpace(course.CourseName)) data.Name = course.CourseName;
            if (!string.IsNullOrWhiteSpace(course.StartDate.ToString())) data.StartDate = course.StartDate;

            _context.SaveChanges();
            return data.Id;
        }

        public void AssignCourse(int StudentId, int CourseId)
        {
            var student = _context.Student.Find(StudentId);
            var course = _context.Courses.Find(CourseId);
            if (student == null || course == null) return;

            var isAssigned = _context.CourseStudent.Any(cs => cs.StudentId == student.Id && cs.CourseId == course.Id);
            if (isAssigned) return;

            var data = new CourseStudent
            {
                CourseId = CourseId,
                StudentId = StudentId
            };

            _context.CourseStudent.Add(data);
            _context.SaveChanges();
        }

        public CourseDetailModel GetCourseDetail(int id)
        {
            var course = _context.Courses.Find(id); //DbSet<Courses>
            if (course == null) return null;

            var student = _context.Student
                .Include(x => x.CourseStudents)
                .Where(x => x.CourseStudents.Any(cs => cs.CourseId == id))
                .Select(x => new StudentViewModel
                {
                    Id = x.Id,
                    FullName = x.LastName + " " + x.FirstName,
                    Age = x.Age,
                    
                }).ToList();


            return new CourseDetailModel
            {
                Id = course.Id,
                Name = course.Name,
                StartDate = course.StartDate,
                Student = student
            };
        }
    


    public int UpgradeGrades(int StudentId, int CourseId, float Assignment, float Final, float Practical)
        {
            var student = _context.Student.Find(StudentId);
            var course = _context.Courses.Find(CourseId);
            var data = _context.CourseStudent.Find(CourseId, StudentId);
            if (student == null || course == null) return 0;
            if(data != null)
            {
                data.Assignment = Assignment;
                data.Final = Final;
                data.Practical = Practical;
                _context.SaveChanges();
                return data.StudentId;
            }
            return 0;
                
        }

        public GradesDetailModel GetGradesDetail(int id)
        {
            var student = _context.Student.Find(id); //DbSet<Courses>
            if (student == null) return null;

            var course = _context.CourseStudent
                .Include(x => x.Course)
                .Where(x => x.StudentId == id)
                .Select(x => new CourseViewModel
                {
                    CourseId = x.CourseId,
                    CourseName = x.Course.Name,
                    StartDate = x.Course.StartDate,
                    Assignment = x.Assignment,
                    Final = x.Final,
                    Practical = x.Practical
                }).ToList();


            return new GradesDetailModel
            {
                Id = student.Id,
                Name = student.FirstName + ' ' + student.LastName,
                courses = course
            };
        }


        public GpaViewModel GetGPA(int id) 
        {
            var student = _context.Student.Find(id);
            if (student == null) return null;

            var gpa = _context.CourseStudent
                .Where(x => x.StudentId == id)
                .Average(x => (x.Practical + x.Final + x.Assignment)  / 3 );

            return new GpaViewModel
            {
                Id = student.Id,
                Name = student.FirstName + ' ' + student.LastName,
                Gpa = gpa
            };
        }
    }

}

