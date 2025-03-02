using System.ComponentModel.DataAnnotations;

namespace ToDoWeb.Application.Dtos
{
    public class SchoolUpdatedModel
    {
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
}
