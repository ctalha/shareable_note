using SharedNote.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SharedNote.Domain.Entites
{
    public class FileDocument : IEntity
    {
        public int Id { get; set; }
        public string CourseTitle { get; set; }
        public string DocumentTitle { get; set; }
        public string FullPath { get; set; }
        public string Path { get; set; }
        public long Length { get; set; }
        public string Extension { get; set; }
        public string DirectoryName { get; set; }
        public string ContentType { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        //Relations id
        public int DepartmentId { get; set; }
        //Relations
        [JsonIgnore]
        public virtual Department Department { get; set; }
    }
}
