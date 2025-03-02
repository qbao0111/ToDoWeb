using System.ComponentModel.DataAnnotations;

namespace ToDoWeb.Application.Dtos
{
    public class CourseCreateModel
    {
        [Required]
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
    }
}
