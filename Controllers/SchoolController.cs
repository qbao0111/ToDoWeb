using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Services;
using ToDoApp.Application.Dtos;
using Microsoft.EntityFrameworkCore.Storage;
using ToDoWeb.Application.Dtos;

namespace ToDoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        public IEnumerable<SchoolViewModel> GetSchools(string? address)
        {
            return _schoolService.GetSchools(address);
        }

        [HttpPost]
        public int PostSchool(SchoolCreatedModel school)
        {
            return _schoolService.PostSchool(school);
        }

        [HttpPut]
        public int PutSchool(SchoolUpdatedModel school)
        {
            return _schoolService.PutSchool(school);
        }

        [HttpDelete]
        public void DeleteSchool(int schoolId)
        {
            _schoolService.DeleteSchool(schoolId);
        }
    }
}