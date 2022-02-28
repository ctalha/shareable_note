using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SharedNote.Domain.Entites;
using System.Reflection;

namespace SharedNotes.Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, UserRole, string>
    {
        private string _connectionString { get; set; }
        protected ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _connectionString = configuration.GetConnectionString("LocalDefaultConnection");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<College> Colleges { get; set; }
        public DbSet<FileDocument> FileDocuments { get; set; }
        public DbSet<Department> Departments { get; set; }

    }
}
