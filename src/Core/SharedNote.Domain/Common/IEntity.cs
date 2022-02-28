using System;

namespace SharedNote.Domain.Common
{
    public interface IEntity
    {
        public DateTime? CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
