namespace ToDoWeb.Application.Dtos
{
    public class SchoolStudentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String Address { get; set; }

        public IEnumerable<StudentViewModel> Students { get; set; }
    }
}
