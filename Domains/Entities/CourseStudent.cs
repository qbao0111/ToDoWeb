namespace ToDoWeb.Domains.Entities
{
    public class CourseStudent
    {
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public float Assignment { get; set; }
        public float Practical { get; set; }
        public float Final { get; set; }
    }
}
