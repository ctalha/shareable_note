using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNote.Application.Dtos
{
    public class FileDto
    {
        public IFormFile File { get; set; }
        public string CourseTitle { get; set; }
        public int DepartmentId { get; set; }
    }
}
