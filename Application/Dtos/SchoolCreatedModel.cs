using System.ComponentModel.DataAnnotations;

namespace ToDoWeb.Application.Dtos
{
    public class SchoolCreatedModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
