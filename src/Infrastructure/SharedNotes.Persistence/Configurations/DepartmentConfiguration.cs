using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedNote.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedNotes.Persistence.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("departments");
            builder.Property(p => p.Name).IsRequired().HasColumnName("name").HasMaxLength(100);
            builder.Property(p => p.Degree).IsRequired().HasColumnName("degree").HasMaxLength(20);
            builder.Property(p => p.Id).IsRequired().HasColumnName("id");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
            builder.Property(p => p.CreateDate).HasColumnName("create_date");
            builder.Property(p => p.DeleteDate).HasColumnName("delete_date");
            builder.Property(p => p.UpdateDate).HasColumnName("update_date");
            builder.Property(p => p.CollegeId).HasColumnName("FK_PK_college_id");
        }
    }
}
