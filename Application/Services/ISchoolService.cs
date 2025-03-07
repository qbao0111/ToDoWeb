using ToDoWeb.Application.Dtos;
using ToDoWeb.Domains.Entities;
using ToDoWeb.Infrastructures;

namespace ToDoApp.Application.Services
{
    public interface ISchoolService
    {
        IEnumerable<SchoolViewModel> GetSchools(string? address);

        public SchoolStudentModel GetSchoolDetail(int id);
        int PostSchool(SchoolCreatedModel school);

        int PutSchool(SchoolUpdatedModel school);

        void DeleteSchool(int schoolId);
    }

    public class SchoolService : ISchoolService
    {
        private readonly IApplicationDbContext _context;

        public SchoolService(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SchoolViewModel> GetSchools(string? address)
        {
            var query = _context.School.AsQueryable();

            if (!string.IsNullOrEmpty(address))
            {
                query = query.Where(x => x.Address.Contains(address));
            }

            return query
                .Select(x => new SchoolViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address
                })
                .ToList();
        }

        public int PostSchool(SchoolCreatedModel school)
        {
            var data = new School
            {
                Name = school.Name,
                Address = school.Address
            };
            var state = _context.Entry(data).State;
            
            _context.School.Add(data);
            state = _context.Entry(data).State;

            _context.SaveChanges();
            
            return data.Id;
        }

        public int PutSchool(SchoolUpdatedModel school)
        {
            var data = _context.School.Find(school.Id);
            if (data == null) return -1;

            if (!string.IsNullOrWhiteSpace(school.Name)) data.Name = school.Name;
            if (!string.IsNullOrWhiteSpace(school.Address)) data.Address = school.Address;

            _context.SaveChanges();
            return data.Id;
        }

        public void DeleteSchool(int schoolId)
        {
            var data = _context.School.Find(schoolId);
            if (data == null) return;

            _context.School.Remove(data);
            _context.SaveChanges();
        }

        public SchoolStudentModel GetSchoolDetail(int schoolId)
        {
            var school = _context.School.Find(schoolId);
            if(school == null)
            {
                return null;
            }


            //SELECT * FROM Student WHERE SchoolId = schoolId
            _context.Entry(school).Collection(x => x.Students).Load();
            var students = school.Students;
            return new SchoolStudentModel
            {
                Id = schoolId,
                Name = school.Name,
                Address = school.Address,
                Students = students.Select(x => new StudentViewModel
                {
                    Id = x.Id,
                    FullName = x.FirstName + " " + x.LastName,
                    Age = x.Age,
                }).ToList()
            };
            
        }
    }
}