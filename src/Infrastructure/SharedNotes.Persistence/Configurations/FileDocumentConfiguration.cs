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
    public class FileDocumentConfiguration : IEntityTypeConfiguration<FileDocument>
    {
        public void Configure(EntityTypeBuilder<FileDocument> builder)
        {
            builder.ToTable("file_documents");

            builder.Property(p => p.Id).IsRequired().HasColumnName("id");
            builder.Property(p => p.Path).IsRequired().HasMaxLength(300).HasColumnName("path");
            builder.Property(p => p.DocumentTitle).IsRequired().HasMaxLength(250).HasColumnName("document_title");
            builder.Property(p => p.CourseTitle).IsRequired().HasMaxLength(100).HasColumnName("course_title");
            builder.Property(p => p.CreateDate).HasColumnName("create_date");
            builder.Property(p => p.DeleteDate).HasColumnName("delete_date");
            builder.Property(p => p.UpdateDate).HasColumnName("update_date");
            builder.Property(p => p.Length).HasColumnName("length").IsRequired();
            builder.Property(p => p.FullPath).HasMaxLength(400).HasColumnName("full_path").IsRequired();
            builder.Property(p => p.Extension).HasColumnName("extension").IsRequired().HasMaxLength(10);
            builder.Property(p => p.DirectoryName).HasColumnName("directory_name").HasMaxLength(150).IsRequired();
            builder.Property(p => p.ContentType).HasColumnName("content_type").HasMaxLength(75).IsRequired();
            builder.Property(p => p.DepartmentId).HasColumnName("FK_PK_department_id");


        }
    }
}
