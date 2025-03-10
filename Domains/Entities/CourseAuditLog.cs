using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoWeb.Infrastructures.Interceptors;

namespace ToDoWeb.Domains.Entities
{
    public class CourseAuditLog :  ICreatedAt, ICreatedBy, IUpdatedAt, IUpdatedBy
    {
        public DateTime CreatedAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UploadedBy { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime UploadedAt { get; set; }
        public int CreatedBy { get; set; }

    }
}
