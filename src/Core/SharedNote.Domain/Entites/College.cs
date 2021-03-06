using SharedNote.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SharedNote.Domain.Entites
{
    public class College : IEntity
    {
        public DateTime? CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        //Relations 
        [JsonIgnore]
        public virtual List<Department> Departments { get; set; }
    }
}
