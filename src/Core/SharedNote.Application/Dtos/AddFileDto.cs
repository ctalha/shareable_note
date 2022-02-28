using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SharedNote.Application.Dtos
{
    public class AddFileDto
    {
        //[Display(Name = "Dosya")]
        //[Required(ErrorMessage = "Dosya Bilgisi Bulunmalıdır.")]
        // [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.jpeg|.odt|.txt|.7z|.zip|.pdf)$", ErrorMessage = "Desteklenmeyen dosya türü, lütfen jpg,jpeg,pdf,odt,txt,7z,zip uzantılı dosylar kullanın.")]
        public IFormFile File { get; set; }

        [Display(Name = "Kurs")]
        [Required(ErrorMessage = "Kurs isim bilgisi gereklidir.")]
        public string CourseTitle { get; set; }

        [Display(Name = "Bölüm")]
        [Required(ErrorMessage = "Bölüm bilgisi gereklidir.")]
        public int DepartmentId { get; set; }
    }
}
