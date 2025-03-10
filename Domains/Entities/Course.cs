using ToDoWeb.Infrastructures.Interceptors;

namespace ToDoWeb.Domains.Entities
{
    public class Course : ICreatedAt, ICreatedBy, IUpdatedAt, IUpdatedBy
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public DateTime StartDate { get; set; }

        public  virtual ICollection<CourseStudent> CourseStudents { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UploadedBy { get; set; }
        public DateTime UploadedAt { get; set ; }
        public int CreatedBy { get ; set  ; }
    }
}
