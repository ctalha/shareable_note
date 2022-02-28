using Microsoft.AspNetCore.Identity;
using SharedNote.Domain.Common;
using System;

namespace SharedNote.Domain.Entites
{
    public class UserRole : IdentityRole, IEntity
    {
        public DateTime? CreateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
