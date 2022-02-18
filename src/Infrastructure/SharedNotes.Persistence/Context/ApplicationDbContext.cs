using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SharedNote.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharedNotes.Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<User,UserRole,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<College> Colleges { get; set; }
        public DbSet<FileDocument> FileDocuments { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}
