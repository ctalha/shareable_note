using SharedNote.Domain.Entites;
using System.Collections.Generic;

namespace SharedNote.Application.Dtos
{
    public class CollegeWithDepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
