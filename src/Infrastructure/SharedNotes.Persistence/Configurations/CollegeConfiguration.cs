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
    public class CollegeConfiguration : IEntityTypeConfiguration<College>
    {
        public void Configure(EntityTypeBuilder<College> builder)
        {
            builder.ToTable("colleges");
            builder.Property(p => p.Id).IsRequired().HasColumnName("id");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100).HasColumnName("name");
            builder.Property(p => p.CreateDate).HasColumnName("create_date");
            builder.Property(p => p.DeleteDate).HasColumnName("delete_date");
            builder.Property(p => p.UpdateDate).HasColumnName("update_date");
        }
    }
}
