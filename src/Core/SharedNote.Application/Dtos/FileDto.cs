using Microsoft.AspNetCore.Http;

namespace SharedNote.Application.Dtos
{
    public class FileDto
    {
        public IFormFile File { get; set; }
        public string CourseTitle { get; set; }
        public int DepartmentId { get; set; }
    }
}
