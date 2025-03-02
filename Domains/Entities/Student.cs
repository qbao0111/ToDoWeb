using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoWeb.Domains.Entities
{
    [Table("Student")]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [MaxLength(255)]
        public string? FirstName { get; set; }

        [Column("Surname")]
        [StringLength(255)]
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        //[MaxLength(2000)]
        //public Byte[] Image { get; set; }

        //[Timestamp]
        //public byte[] RowVersion { get; set; }

        [ConcurrencyCheck]
        public Decimal Balance { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }


        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Age { get; set ; }

        [ForeignKey("School")]
        public int SId { get; set; }
        public School School { get; set; }

        public ICollection<CourseStudent> CourseStudents { get; set; }
    }
}
