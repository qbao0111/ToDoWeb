using Microsoft.AspNetCore.Mvc;
using ToDoWeb.Application.Dtos;
using ToDoWeb.Application.Services;

namespace ToDoWeb.Controllers
{
    
        [ApiController]
        [Route("[controller]")]
        public class CourseController : ControllerBase
        {
            private readonly ICourseService _courseService;

            public CourseController(ICourseService courseService)
            {
                _courseService = courseService;
            }

            [HttpGet]
            public IEnumerable<CourseViewModel> GetSchools(string? courseId)
            {
                return _courseService.GetCourses(courseId);
            }

            [HttpPost]
            public int PostCourse(CourseCreateModel course)
            {
                return _courseService.PostCourse(course);
            }

            [HttpPut]
            public int PutCourse(CourseUpdateModel course)
            {
                return _courseService.PutCourse(course);
            }

            [HttpDelete]
            public void DeleteCourse(int courseId)
            {
                _courseService.DeleteCourse(courseId);
            }

            [HttpGet("{id}")]
            public CourseDetailModel GetCourseDetail(int id)
            {
                return _courseService.GetCourseDetail(id);
            }

            [HttpPost("assign")]
            public void AssignCourse(int StudentId, int CourseId)
            {
                _courseService.AssignCourse(StudentId, CourseId);
            }
        
    }
    }