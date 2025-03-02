namespace ToDoWeb.Application.Dtos
{
    public class CourseDetailModel
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public DateTime StartDate { get; set; }

        public List<StudentViewModel> Student { get; set; }
    }
}
