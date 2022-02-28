using SharedNote.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SharedNote.Domain.Entites
{
    public class Department : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Degree { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        //Relations Id
        public int CollegeId { get; set; }
        //Relations
        [JsonIgnore]
        public virtual College College { get; set; }
        [JsonIgnore]
        public virtual List<FileDocument> FileDocuments { get; set; }
    }
}
